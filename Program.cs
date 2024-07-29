using OllamAgent;

class Program {
    static async Task Main(string[] args)
    {
        var d = new Discussion("Rédiger une dissertation de philosophie.");
        var rep = await d.GetAgent("001").ExecuteTask();
    }
}