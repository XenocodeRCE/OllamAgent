using Newtonsoft.Json;
using OllamaSharp;

namespace OllamAgent
{
    internal class Discussion {
        public List<Agent> agents { get; set; }
        public string mission { get; set; }
        public List<Message> historique { get; set; }

        public OllamaApiClient ollama { get; set; }
        public Discussion(string m_mission) {
            agents = new List<Agent>();
            historique = [];
            mission = m_mission;

            Loadagents();

            var uri = new Uri("http://localhost:11434");
            ollama = new OllamaApiClient(uri);
            ollama.SelectedModel = "llama3.1:8b-instruct-q8_0";
        }

        public void Loadagents()
        {
            foreach (var fileName in Directory.GetFiles("Agents/", "*.json"))
            {
                var data = File.ReadAllText(fileName);
                Agent agent = JsonConvert.DeserializeObject<Agent>(data);
                agent.discussion = this;
                agents.Add(agent);
                Logger.Log($"Nouvel agent trouvé ({agent.nom})", Logger.Type.NORMAL);
            }
        }

        public Agent? GetAgent(string nom)
        {
            foreach (var agent in agents)
            {
                if (agent.nom == nom)
                    return agent;
            }
            return null;
        }
    }
}