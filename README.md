# VR-Therapeut

Deze applicatie maakt gebruik van een LLM in VR om zo een persoonlijke assistent te simuleren. Binnen de applicatie zijn er mogelijkheden om gebruik te maken van een therapeut of studieadviseur.
Verder is het mogelijk om deze applicatie als template te gebruiken voor een ander soort assistent, door zelf een prompt te uploaden.

## API key
Om de applicatie te laten werken moet de gebruiker zelf een API key aanvragen bij OpenAI en deze verwerken in de applicatie. Maak een file genaamd "APIkey.cs" aan en voeg hieraan een *public const string api_key* en eventueel een *public const string org_key*.
```cs
public static class APIKey
{
    public const string api_key = "sk-...";
    public const string org_key = "org-...";
}
```
**Zorg dat deze file altijd in de gitignore staat indien dit project geüpload wordt naar Git.**

## Een prompt uploaden
Indien de gebruiker een nieuw prompt wil uploaden, kan de gebruiker een .txt bestand aanmaken en deze uploaden naar de headset. Binnen de applicatie kan in de geavanceerde instellingen een file browser geopend worden om het geüploade .txt bestand te zoeken.
Zodra het bestand geselecteerd is, krijgt de gebruiker een zwart scherm. Dit is een fout in de file browser die nog niet opgelost is. Door eenmaal op de Oculus knop te klikken, is dit probleem verholpen en werkt alles naar behoren.
