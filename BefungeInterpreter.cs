namespace DoctorFe.Executable
{
    internal class BefungeInterpreter
    {
        static void Main()
        {
            Befunge interpreter = new(">0\"!dlroW olleH\"> :# ,# _@");
            interpreter.InterpretToEnd();
        }
    }
}
