<p align="center">
  <img src="https://raw.githubusercontent.com/XenocodeRCE/OllamAgent/main/logo.svg" alt="Logo" style="width:500px;">
</p>

# OllamAgent 🦙
Ollama agent solution


## Agents

```
{
  "type": "WORKER",
  "nom": "élève",
  "tâche": "Formule une explication du sujet en utilisant au moins quatre exemples pour que tout le monde comprenne bien les concepts en jeu dans le sujet. Ta réflexion doit être étayée par des analyses conceptuelles, des références et des exemples philosophiques, classiques, pertinents et élaborés. N'hésite pas à puiser dans le corpus et les doctrines philosophiques des auteurs au programme : Les présocratiques ; Platon ; Aristote ; Zhuangzi ; Épicure ; Cicéron ; Lucrèce ; Sénèque ; Épictète ; Marc Aurèle ; Nāgārjuna ; Sextus Empiricus ; Plotin ; Augustin ; Avicenne ; Anselme ; Averroès ; Maïmonide ; Thomas d’Aquin ; Guillaume d’Occam. N. Machiavel ; M. Montaigne (de) ; F. Bacon ; T. Hobbes ; R. Descartes ; B. Pascal ; J. Locke ; B. Spinoza ; N. Malebranche ; G. W. Leibniz ; G. Vico ; G. Berkeley ; Montesquieu ; D. Hume ; J.-J. Rousseau ; D. Diderot ; E. Condillac (de) ; A. Smith ; E. Kant ; J. Bentham. G.W.H. Hegel ; A. Schopenhauer ; A. Comte ; A.- A. Cournot ; L. Feuerbach ; A. Tocqueville (de) ; J.-S. Mill ; S. Kierkegaard ; K. Marx ; F. Engels ; W. James ; F. Nietzsche ; S. Freud ; E. Durkheim ; H. Bergson ; E. Husserl ; M. Weber ; Alain ; M. Mauss ; B. Russell ; K. Jaspers ; G. Bachelard ; M. Heidegger ; L. Wittgenstein ; W. Benjamin ; K. Popper ; V. Jankélévitch ; H. Jonas ; R. Aron ; J.-P. Sartre ; H. Arendt ; E. Levinas ; S. de Beauvoir ; C. Lévi-Strauss ; M. Merleau-Ponty ; S. Weil ; J. Hersch ; P. Ricœur ; E. Anscombe ; I. Murdoch ; J. Rawls ; G. Simondon ; M. Foucault ; H. Putnam.",
  "contexte": {
    "modèles": [
      "...",
      "..."
    ],
    "besoins": [
      {
        "nom": "sujet",
        "contenu": ""
      }
    ]
  }
}
```
**Explications :**

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
var rep = await d.GetAgent("élève").ExecuteTask();
// récupère les 'besoins' chez les autres agents, s'il n'y a pas : demande à l'utilisateur
```

# TODO
- [ ] llama3.1 instruct parser : if LLM sends back instructions
