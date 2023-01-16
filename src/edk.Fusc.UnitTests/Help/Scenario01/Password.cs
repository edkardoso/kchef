using System.Text;

namespace edk.Fusc.UnitTests.Help.Scenario01;

public static class Password
{
    public static Task<string> Generate() => Generate(6);

    public static Task<string> Generate(int lenght)
    {
        return Task.Run(() =>
        {

            const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            StringBuilder container = new StringBuilder();
            Random rnd = new();

            for (int i = 0; i < lenght; i++)
            {
                int index = rnd.Next(chars.Length);
                container.Append(chars[index]);
            }

            return container.ToString();
        });
    }
}

