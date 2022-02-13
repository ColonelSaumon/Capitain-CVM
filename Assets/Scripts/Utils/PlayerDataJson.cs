using System.Collections.Generic;
/// <summary>
/// Offre un moteur de lecture/écriture du JSON
/// pour l'objet <code>PlayerData</code>
/// </summary>
public static class PlayerDataJson
{
    /// <summary>
    /// Sérialise un objet de type PlayerData au format JSON
    /// </summary>
    /// <param name="data">Paramètre à sérialiser</param>
    /// <returns>La chaîne contenant le format JSON</returns>
    public static string WriteJson(PlayerData data)
    {
        string tab = "\t";
        string newline = "\n";
        string json = "{" + newline;
        json += tab + "\"vie\":" + data.Vie + "," + newline;
        json += tab + "\"energie\":" + data.Energie + "," + newline;
        json += tab + "\"score\":" + data.Score + "," + newline;
        json += tab + "\"chestOpenList\":[";
        if (data.ListeCoffreOuvert.Length > 0)
        {
            json += newline;
            for (int i = 0; i < data.ListeCoffreOuvert.Length; i++)
            {
                string chestData = data.ListeCoffreOuvert[i];
                json += tab + tab + "\"" + chestData + "\"";
                if (i + 1 < data.ListeCoffreOuvert.Length)
                    json += ",";
                json += newline;
            }
            json += tab + "]" + newline;
        }
        else json += "]" + newline;
        json += "}";
        return json;
    }

    /// <summary>
    /// Récupère un objet PlayerData depuis un format JSON
    /// </summary>
    /// <param name="json">Format JSON à traiter</param>
    /// <returns>L'objet converti</returns>
    /// <exception cref="JSONFormatExpcetion">La chaîne n'est pas
    /// au format JSON</exception>
    /// <exception cref="System.ArgumentException">La chaîne fournit
    /// ne peut pas contenir un format JSON</exception>
    public static PlayerData ReadJson(string json)
    {
        if (json.Length < 2 || string.IsNullOrEmpty(json))
            throw new
                System.ArgumentException("La chaîne n'est pas valide");
        if (json[0] != '{')
            throw new JSONFormatExpcetion();
        json = json.Replace("\t", string.Empty);

        int vie = 0, energie = 0, score = 0;
        List<string> chests = new List<string>();
        string[] lignes = json.Split('\n');
        
        for(int i = 1; i < lignes.Length || lignes[i] != "}"; i++)
        {
            if (lignes[i] == "}") break;

            string[] parametre = lignes[i].Split(':');
            if (parametre.Length != 2)
                throw new JSONFormatExpcetion();
            switch(parametre[0])
            {
                case "\"vie\"":
                    vie = int.Parse(parametre[1]
                        .Replace(",", string.Empty));
                    break;
                case "\"energie\"":
                    energie = int.Parse(parametre[1].Replace(",", string.Empty));
                    break;
                case "\"score\"":
                    score = int.Parse(parametre[1].Replace(",", string.Empty));
                    break;
                case "\"chestOpenList\"":
                    if (parametre[1] == "[]")
                        break;
                    else if (parametre[1] != "[")
                        throw new JSONFormatExpcetion();
                    while(lignes[++i] != "]")
                    {
                        chests.Add(lignes[i]
                            .Replace(",", string.Empty)
                            .Replace("\"", string.Empty));
                    }
                    break;
            }
        }

        return new PlayerData(vie, energie, score, ChestList: chests);
    }
}

public class JSONFormatExpcetion : System.Exception
{
    public JSONFormatExpcetion()
        : base("La chaîne n'est pas un format reconnu") { }
}
