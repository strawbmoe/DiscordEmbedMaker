using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace WebHookGenerator
{
    class Program
    {


        static void Main(string[] args)
        {
            Console.WriteLine("Discord Embed Tool by kyo#4444 // @strawbmoe");

            var utils = new Utils();
            var Embeds = new List<Embed>();

            while (utils.CheckIfNewEmbed())
            {
                var embed = new Embed();
                if (utils.GetEmbedType() == 1) // just text
                {

                    embed.title = utils.GetTitle();
                    embed.description = utils.GetDescription();
                    embed.color = utils.GetEmbedColor();
                }
                else if (utils.GetEmbedType() == 2) // just image
                {
                    embed.color = utils.GetEmbedColor();
                    embed.image = new Image
                    {
                        url = utils.GetEmbedImage()
                    };
                }
                else if (utils.GetEmbedType() == 3) // image and text
                {
                    embed.title = utils.GetTitle();
                    embed.description = utils.GetDescription();
                    embed.color = utils.GetEmbedColor();
                    embed.image = new Image
                    {
                        url = utils.GetEmbedImage()
                    };
                }
                Embeds.Add(embed);
            }
            
            var jsonString = JsonConvert.SerializeObject(Embeds, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
            jsonString = "{ \"embeds\":" + jsonString + "}";
            Console.WriteLine(jsonString);

            Console.ReadKey();
        }

    }


    class Embed
    {
        public string title { get; set; }
        public string description { get; set; }
        public int color { get; set; }
        public Image image { get; set; }
        //public int MyProperty { get; set; }

    }
    class Image
    {
        public string url { get; set; }

    }

    class Utils
    {
        public int GetEmbedType()
        {
            int input = Convert.ToInt16(GetUserInput("Please enter the type of embed you want:\n1) Just text\n2) Just Image\n3) Image and text"));
            while (input > 3 || input < 1)
            {
                input = Convert.ToInt16(GetUserInput("Please input title for the Embed! Leave blank for none"));
            }
            return input;
        }

        public string GetUserInput(string output, bool allowedempty = true)
        {

            Console.WriteLine(output);
            var input = Console.ReadLine();
            //input = input.Replace(" ", "");

            if (allowedempty)
            {
                return input;
            }

            while ((!allowedempty) && (input == "" || input == null))
            {
                Console.WriteLine(output);
                input = Console.ReadLine();
                input = input.Replace(" ", "");
            }

            return input;
        }

        public int GetEmbedColor()
        {
            var input = GetUserInput("Please input Embed Side RGB color (without hashtag): ");
            Regex rx = new Regex("^([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$"); //https://www.geeksforgeeks.org/how-to-validate-hexadecimal-color-code-using-regular-expression/#:~:text=Create%20a%20regular%20expression%20to,the%20starting%20of%20the%20string.
            while (!rx.IsMatch(input))
            {
                input = GetUserInput("Please input Embed Side RGB color (without hashtag): ");
            }

            var output = Convert.ToInt32(input, 16);

            return output;
        }

        public string GetTitle()
        {
            var input = @GetUserInput("Please input title for the Embed! Leave blank for none");
            return input;
        }

        public string GetEmbedImage()
        {
            var input = @GetUserInput("Please input image url for the Embed! Leave blank for none!");
            return input;
        }

        public string GetDescription()
        {
            var input = @GetUserInput("Please input description for the Embed! Leave blank for none");
            return input;
        }

        public bool CheckIfNewEmbed()
        {
            var input = GetUserInput("Would you like a new embed?\ny) yes\nn)No");
            while (input.ToLower() != "n" && input.ToLower() != "y")
            {
                input = GetUserInput("Would you like a new embed?\ny) yes\nn)No");
            }
            if (input == "n")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
