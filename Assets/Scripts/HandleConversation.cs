using OpenAI;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using Newtonsoft.Json.Linq;
using Samples.Whisper;
using ReadyPlayerMe.Core;
using TMPro;
using UnityVAD;
using System.Collections;

public class HandleConversation : MonoBehaviour
{
    [SerializeField] private UseSettings settings;
    private VoiceHandler voiceHandler;
    private Animator animator;
    private GameObject avatar;
    private readonly string fileName = "output.wav";

    private AudioClip record_clip;
    private List<ChatMessage> messages = new List<ChatMessage>();
    private string api_key = APIKey.api_key;
    private string org_key = APIKey.org_key;
    private OpenAIApi openai;
    private CancellationTokenSource token = new CancellationTokenSource();
    private string prompt;

    private string model;
    private AudioRecorder audioRecorder;
    private AudioSource audioSource;

    private bool isProcessing = false;

    void Start()
    {
        avatar = settings.GetAvatar();
        audioSource = avatar.GetComponent<AudioSource>();
        // Start a new API session
        openai = new OpenAIApi(api_key, org_key);

        // Change the initial prompt based on the chosen role
        string role = settings.GetRole();
        if (settings.GetPrompt() == "")
        {
            if (role == "therapist")
            {
                prompt = "Act as a therapist, and reply to the patient's concerns. Don't break character. Don't ever mention that you are an AI model and do not refer to professionals, try and help the user yourself. Gather enough information before trying to provide a solution. Keep your answers short and to the point, while also being empathetic. Summarize the users input as little as possible. Respond in the language you are being spoken to.";
            }
            else
            {
                prompt = "Act as a student counselor for the University of Amsterdam. Be inviting but concise.Summarize the users input as little as possible. If the student talks about mental illness, refer to: Professor Rob Belleman. If the student talks about delays in their study, refer to: Professor Jens Dijkgraaf. Do not offer to make an appointment.";
            }
        }

        // If there was a personalized prompt provided, use this.
        else
        {
            prompt = settings.GetPrompt();
        }
        // If the user has chosen the AI to take initiative, take initiative in the chosen language.
        if (settings.GetInitiative())
        {
            if (settings.GetLanguage() == "english")
            {
                isProcessing = true;
                SendToGPT("The user walks into the room, you should start the conversation with a greeting and invite them to share.");
            }
            else
            {
                isProcessing = true;
                SendToGPT("De gebruiker loopt de kamer binnen, je moet het gesprek beginnen met een begroeting en hen uitnodigen om te delen.");
            }
        }
        audioRecorder = AudioRecorder.Instance;
        audioRecorder.MIC_SpeechEventHandler += OnMicSpeechEvent;
        audioRecorder.MIC_DataAddedEventHandler += OnMicDataAddedEvent;
    }

    void OnMicSpeechEvent(SpeechEvent speechEvent)
    {
        if (isProcessing) return;
    }
    void OnMicDataAddedEvent(float[] data)
    {
        if (isProcessing) return;
        isProcessing = true;
        var filename = DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".wav";
        record_clip = VADUtil.CreateAudioClip(filename, data,
            audioRecorder.m_AudioSource.clip.channels, (int)audioRecorder.samplingRate);
        GetTranscript(record_clip);
    }

    private async void GetTranscript(AudioClip clip)
    {
        // Save the audio clip to a file
        byte[] data = SaveWav.Save(fileName, clip);
        // Create audio transcription API request
        var req = new CreateAudioTranscriptionsRequest
        {
            FileData = new FileData() { Data = data, Name = "transcript.wav" },
            Model = "whisper-1",
        };

        var res = await openai.CreateAudioTranscription(req);
        Debug.Log($"Transcriptie: {res.Text}");
        SendToGPT(res.Text);
    }

    // Instant
    private async void SendToGPT(string text)
    {
        // Create a new message using the users input
        var newMessage = new ChatMessage()
        {
            Role = "user",
            Content = text
        };

        // If there are no messages, add the prompt to the message
        if (messages.Count == 0) newMessage.Content = $"{prompt}\n{text}";

        // Add the message to the list of messages
        messages.Add(newMessage);

        model = settings.GetModel();
        var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
        {
            Model = model,
            // Send all messages to ensure context is maintained
            Messages = messages
        });

        // If there are choices, add the first choice to the messages list
        if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
        {
            var message = completionResponse.Choices[0].Message;
            message.Content = message.Content.Trim();

            messages.Add(message);
            // Speak the response
            SpeakResponse(message.Content);
        }
        else
        {
            Debug.LogWarning("No text was generated from this prompt.");
        }
    }

    private async void SpeakResponse(string text)
    {
        // Get the voice from the user settings
        string voice = settings.GetVoice();
        var request = new CreateTextToSpeechRequest
        {
            Input = text,
            Model = "tts-1",
            Voice = voice
        };

        var response = await openai.CreateTextToSpeech(request);
        voiceHandler = avatar.GetComponent<VoiceHandler>();
        animator = avatar.GetComponent<Animator>();

        // Get the length of the response.AudioClip
        float length = response.AudioClip.length;

        // Play the response as audio and transition to the talking animation
        voiceHandler.PlayAudioClip(response.AudioClip);
        StartCoroutine(ChangeLayerWeight(1.0f, 1.5f, 1));
        // Transition back to idle animation after the response has finished playing
        Invoke("PlayIdle", length);
    }

    private void OnDestroy()
    {
        token.Cancel();
    }


    private IEnumerator ChangeLayerWeight(float targetWeight, float transitionTime, int layerIndex)
    {
        float elapsedTime = 0.0f;
        float currentWeight = animator.GetLayerWeight(layerIndex);

        while (elapsedTime < transitionTime)
        {
            // Calculate the new weight based on lerp
            float newWeight = Mathf.Lerp(currentWeight, targetWeight, elapsedTime / transitionTime);

            // Set the new weight for the talking animation layer
            animator.SetLayerWeight(layerIndex, newWeight);

            // Increment time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure the target weight is reached exactly
        animator.SetLayerWeight(layerIndex, targetWeight);
    }

    private void PlayIdle()
    {
        isProcessing = false;
        StartCoroutine(ChangeLayerWeight(0.0f, 1.5f, 1));
    }

    public void ClearContext()
    {
        messages.Clear();
    }
}
