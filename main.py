import ollama
import json
import os


class AgentManager:
    def __init__(self, agent_folder_path):
        self.agent_folder_path = agent_folder_path
        self.agents = {}

    def load_agents(self):
        for file_name in os.listdir(self.agent_folder_path):
            if file_name.endswith(".json"):
                file_path = os.path.join(self.agent_folder_path, file_name)
                with open(file_path, 'r') as f:
                    data = json.load(f)
                    agent = Agent.from_dict(data)
                    self.agents[agent.nom] = agent

    def get_agent(self, nom):
        return self.agents.get(nom)

    
class Agent:
    def __init__(self, nom, tache):
        self.nom = nom
        self.tache = tache
        self.contexte = None

    @property
    def contexte(self):
        return self._contexte

    @contexte.setter
    def contexte(self, valeur):
        self._contexte = valeur

    def to_dict(self):
        return {
            'nom': self.nom,
            'tache': self.tache,
            'contexte': self.contexte
        }

    @classmethod
    def from_dict(cls, data):
        agent = cls(data['nom'], data['tache'])
        agent.contexte = data['contexte']
        return agent

    def to_json(self):
        return json.dumps(self.to_dict())

    @classmethod
    def from_json(cls, json_data):
        data = json.loads(json_data)
        return cls.from_dict(data)

    history = []
    def ask_AI(msg):
        while True:
            try:
                history.append({"role": "user", "content": msg})
                chunky = ""
                stream = ollama.chat(
                    model='llama3.1:8b-instruct-q8_0',
                    messages=history,
                    stream=True,
                )
                for chunk in stream:
                    print(chunk['message']['content'], end='', flush=True)
                    chunky = chunky + chunk['message']['content']
                response = chunky
                history.append({"role": "assistant", "content": response})

                return response
            except Exception as e:
                print("Une erreur s'est produite :", str(e))
                print("La fonction va recommencer...")


    def Execute(self):
        response = ask_AI(self.tache)
        return response

    def save(self):
        data = self.to_dict()
        folder_path = 'Agents/'
        if not os.path.exists(folder_path):
            os.makedirs(folder_path)
        file_name = f"{self.nom}.json"
        file_path = os.path.join(folder_path, file_name)
        with open(file_path, 'w') as f:
            json.dump(data, f)
        print(f"L'agent {self.nom} a été enregistré dans le fichier {file_name}")



# Utilisation
agent_manager = AgentManager('Agents/')
agent_manager.load_agents()

for nom, agent in agent_manager.agents.items():
    print(f"Agent {nom}: {agent.tache}")

    
# Création d'un agent appelé "Mr Phi"
#Mr_Phi = Agent("Mr Phi", "Rédiger une dissertation de philosophie")
##print(Mr_Phi.nom)  # Rédige une dissertation de philosophie
##print(Mr_Phi.tache)  # Rédiger une dissertation de philosophie
##
### Exportation en JSON
##json_data = Mr_Phi.to_json()
##print(json_data)
##
### Importation à partir du JSON
##Mr_Phi_from_json = Agent.from_json(json_data)
##print(Mr_Phi_from_json.nom)  # Mr Phi
##print(Mr_Phi_from_json.tache)  # Rédiger une dissertation de philosophie
##print(Mr_Phi_from_json.contexte)  # None
##Mr_Phi.save()
