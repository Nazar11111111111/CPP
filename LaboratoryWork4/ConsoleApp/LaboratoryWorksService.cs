using LaboratoryWorks;

namespace ConsoleApp;


internal static class LaboratoryWorksService
{
    private static readonly Func<string, string>[] _runMethods =
    [
        LaboratoryWork1.Run,
        LaboratoryWork2.Run,
        LaboratoryWork3.Run
    ];


    public static int Number => _runMethods.Length;


    public static string Run(int number, string inputText)
    {
        try
        {
            return _runMethods[number - 1](inputText);
        }
        catch (InputException exception)
        {
            return exception.Message;
        }
    }
}