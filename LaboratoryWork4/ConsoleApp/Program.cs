using ConsoleApp;
using McMaster.Extensions.CommandLineUtils;


var app = new CommandLineApplication
{
    Name = "LabRunner",
    Description = "A console tool for running laboratory works",
    UnrecognizedArgumentHandling = UnrecognizedArgumentHandling.Throw
};

app.Command("version", cmd =>
{
    cmd.Description = "Display version information";
    cmd.OnExecute(() =>
    {
        Console.WriteLine("LabRunner v1.0 by Nazar Leonchuk");
        return 0;
    });
});

app.Command("run", cmd =>
{
    cmd.Description = "Run a laboratory work";
    var labOption = cmd.Argument(
        "<LAB_NUMBER>",
        "Lab number (lab1, lab2, or lab3)"
    )
        .IsRequired();
    var inputOption = cmd.Option(
        "-i|--input <INPUT_FILE>",
        "Input file",
        CommandOptionType.SingleValue
    );
    var outputOption = cmd.Option(
        "-o|--output <OUTPUT_FILE>",
        "Output file",
        CommandOptionType.SingleValue
    );

    cmd.OnExecute(() =>
    {
        var validLabNumberTexts = Enumerable.Range(1, LaboratoryWorksService.Number)
            .Select(n => $"lab{n}");
        if (!validLabNumberTexts.Contains(labOption.Value))
        {
            Console.WriteLine("Invalid laboratory work number.");
            return 1;
        }
        int labNumber = int.Parse(labOption.Value![3..]);
        var inputFilePath = ResolveFilePath(inputOption.Value(), "INPUT.TXT");
        var outputFilePath = ResolveFilePath(outputOption.Value(), "OUTPUT.TXT");

        Console.WriteLine($"Input file: {inputFilePath}");
        Console.WriteLine($"Output file: {outputFilePath}");

        string inputText;
        try
        {
            inputText = File.ReadAllText(inputFilePath);
        }
        catch (IOException exception)
        {
            Console.WriteLine($"Error reading file: {exception.Message}");
            return exception.HResult;
        }

        Console.WriteLine($"Running lab №{labNumber}...");
        var outputText = LaboratoryWorksService.Run(labNumber, inputText);

        try
        {
            File.WriteAllText(outputFilePath, outputText);
        }
        catch (IOException exception)
        {
            Console.WriteLine($"Error writing file: {exception.Message}");
            return exception.HResult;
        }

        return 0;
    });
});

app.Command("set-path", cmd =>
{
    cmd.Description = "Set the path for input and output files";
    var pathOption = cmd.Option(
        "-p|--path <LAB_PATH>",
        "Path to the folder with input and output files",
        CommandOptionType.SingleValue
    )
        .IsRequired();

    cmd.OnExecute(() =>
    {
        Environment.SetEnvironmentVariable(
            "LAB_PATH",
            pathOption.Value(),
            EnvironmentVariableTarget.User
        );
        Console.WriteLine($"LAB_PATH set to: {pathOption.Value()}");
        return 0;
    });
});

app.OnExecute(() =>
{
    Console.WriteLine("Invalid command. Use 'version', 'run', or 'set-path'.");
    app.ShowHelp();
    return 0;
});


try
{
    return app.Execute(args);
}
catch (CommandParsingException exception)
{
    Console.WriteLine(exception.Message);
    app.ShowHelp();
}
catch
{
    app.ShowHelp();
}
return 1;


static string ResolveFilePath(string? consoleValue, string defaultFileName)
{
    if (!string.IsNullOrEmpty(consoleValue))
    {
        return consoleValue;
    }

    var labPath = Environment.GetEnvironmentVariable("LAB_PATH");
    if (!string.IsNullOrEmpty(labPath))
    {
        return Path.Combine(labPath, defaultFileName);
    }

    var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    return Path.Combine(homeDirectory, defaultFileName);
}