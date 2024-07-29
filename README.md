<p align="center">
  <img src="https://raw.githubusercontent.com/XenocodeRCE/OllamAgent/main/logo.svg" alt="Logo" style="width:500px;">
</p>

# OllamAgent 🦙
Ollama agent solution


## Agents

```
{
  "type": "WORKER",
  "nom": "001",
  "tâche": "Analyse des données",
  "contexte": {
    "modèles": [
      "Modèle A",
      "Modèle B"
    ],
    "besoins": [
      {
        "nom": "Besoin1",
        "contenu": ""
      },
      {
        "nom": "Besoin2",
        "contenu": "Instructions spécifiques"
      }
    ]
  }
}
```
**Explications : **

- type : WORKER (agent) / SUPERVISOR (évaluateur)
- nom : _
- tâche : ce que l'agent doit faire, ce pour quoi il est fait
- contexte :
  - modèles : utilisés comme "bons" modèles pour l'agent
  - besoins : ce dont l'agent a besoin pour faire sa tâche

  
**À placer dans le dossier** `bin/Agents/`.

## USAGE

```csharp
var d = new Discussion("Rédiger une dissertation de philosophie.");
// récupère les agents dans le dossier /Agents automatiquement
var rep = await d.GetAgent("001").ExecuteTask();
// récupère les 'besoins' chez les autres agents, s'il n'y a pas : demande à l'utilisateur
```

# TODO
- [ ] llama3.1 instruct parser : if LLM sends back instructions
