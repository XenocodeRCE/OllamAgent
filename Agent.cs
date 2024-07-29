using OllamaSharp;

namespace OllamAgent {
    internal class Agent {
        public enum Type {
            WORKER,
            SUPERVISOR
        }
        public required Type type { get; set; }
        public required string nom { get; set; }
        public required string tâche { get; set; }
        public required Contexte contexte { get; set; }
        public required Discussion discussion { get; set; }
        public Agent(string _nom)
        {

        }

        public void CheckBesoin()
        {
            foreach (var besoin in contexte.besoins)
            {
                if (string.IsNullOrEmpty(besoin.contenu))
                {
                    foreach (var agent in discussion.agents)
                    {
                        var besoinTrouvé = agent.contexte.besoins.Find(b => b.nom == besoin.nom && !string.IsNullOrEmpty(b.contenu));
                        if (besoinTrouvé != null)
                        {
                            besoin.contenu = besoinTrouvé.contenu;
                            Logger.Log($"L'agent ({nom}) vient de récupérer la valeur de '{besoinTrouvé.nom}' chez l'agent ({agent.nom})", Logger.Type.INFO);
                            break;
                        }
                    }
                }
            }


            foreach (var besoin in contexte.besoins)
            {
                if (string.IsNullOrEmpty(besoin.contenu))
                {
                    Logger.Log($"L'agent {nom} a besoin de la valeur de la propriété '{besoin.nom}' :", Logger.Type.WARN);
                    besoin.contenu = "" + Console.ReadLine();
                }
            }
        }


        public async Task<string> ExecuteTask(string prompt = null)
        {
            CheckBesoin();

            prompt = $"Tu es une IA dont la tâche est : '{tâche}'. Tu participe à un plus vaste projet qui est : '{discussion.mission}' {prompt}\n\n";


            prompt += $"Pour t'aider dans ta tâche, voici quelques informations à prendre en compte : \n\n";
            foreach (Besoin besoin in contexte.besoins)
            {
                prompt += $"{besoin.nom}:{besoin.contenu}\n";
            }

            prompt += $"\nPour rendre ta production parfaite, voici quelques modèles qui ont été jugés parfaits pour t'en inspirer : \n\n";
            foreach (string modèle in contexte.modèles)
            {
                prompt += $"{modèle}\n";
            }


            string completeResponse = "";
            var abc = await discussion.ollama.StreamCompletion(prompt, null, stream =>
            {
                completeResponse += stream.Response;
            });

            return completeResponse;
        }


        public async Task<string> Ask(Agent agent2, Message msg)
        {
            string prompt = " ";
            switch (agent2.type)
            {
                case Type.WORKER:
                    return await agent2.ExecuteTask(msg.contenu);
                case Type.SUPERVISOR:
                    prompt = $"Je suis une IA dont la tâche est : '{tâche}'. Pour répondre à ma tâche, je viens de produire un travail et je veux que tu évalue ce travail en me donnant une note entre 0 et 20 uniquement. Voilà la production que tu dois noter : ```{msg.contenu}```.\n\nPour t'aider dans ton évaluation, voici ce qu'attend mon prof qui m'a donné des modèles : \n\n```{String.Join("\n\n", contexte.modèles.ToArray())}```.\n\nNe répond que la note dans ce format : `x/20` où x est la note.";
                    break;
            }


            string completeResponse = "";
            var abc = await discussion.ollama.StreamCompletion(prompt, null, stream =>
            {
                completeResponse += stream.Response;
            });

            return completeResponse;
        }
    }
}