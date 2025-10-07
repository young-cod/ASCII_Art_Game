using System;
using System.Threading;
using System.Linq;

namespace AsciiArt
{
    class GameOverScene : Scene
    {
        readonly string[] skelton = new string[] {
            "                                   ###########                                   ",
            "                             ######.         ######                              ",
            "                          ###                       ###                          ",
            "                       ###                             ##                        ",
            "                     ###                                 ##                      ",
            "                    #   #                              -.  ##                    ",
            "                  -#     #                             #    ##                   ",
            "                  #       #                           #      -#                  ",
            "                 #         ##                       #         ##                 ",
            "                 #                                             #                 ",
            "                ##            #                   #            #                 ",
            " #########      #-       #    # #              #      ##       #       ########  ",
            "#         #     ##     .                                      -#     +#        ##",
            "###     -##     ##            #                   +            #      #       # #",
            "## ### ####-     # #    ##############     ##############     ##     ####  ## ## ",
            "   ###    ####   #    ################     ###############  # ##  ####      #    ",
            "    ###       ##### # ################    ################ .. ###+#      ####    ",
            "  ##              # # ################ #   ############### #  #             ##   ",
            "  ### #   ######  ### ############### #   ################ ####  #####+  ## .#   ",
            "   ########   ######  ##############  ####  ##############  ######   +########   ",
            "     ######    ####    #########-     #####     ##########   #####   .######     ",
            "                 ##      #      #.   ########  #  .   ##      #                  ",
            "                  #.#               ########               #+##                  ",
            "                   ##+########      #### ####     +######## #                    ",
            "                     ##########   # #### +###   # #########+                     ",
            "                    ##+ ######+## ##         # ##### ###  ###                    ",
            "     ####      ####   #   # ##  #               - #####  .#   ####      ####     ",
            "    #     ####        ##  ###                      ## # ###        ####    -#    ",
            "   #-   ##       ##### ## ###-#     #  #        .#### # # ######       ##  #+#   ",
            "   #####      ####  ## #  # ##  #### # #  ## ####  ###. # ### -####      #####   ",
            "     #     #### ###    #   ## ## #   #  #    #  ######  ##   ###   ##      #     ",
            "  ##     ### ###      ##    ###  # ########### -# ##     #      ###  ###    ##   ",
            "###     .## ##         ##      ### # #  #    ###-      ###         ## ##     +## ",
            "#      #####+           ##        # # ## # #          ###           #####       #",
            "###    ##-#               #            +             .#              # ###   # ##",
            " ###### ##                #####                  ##+##                 # ######  ",
            "                            ###-###           ######                             ",
            "                               ####+   ###  +####.                               ",
            "                                  #############                                  ",
        };

        readonly string[] gameOver = new string[] {
                "  ____                         ___                 _ ",
                " / ___| __ _ _ __ ___   ___   / _ \\__   _____ _ __| |",
                "| |  _ / _` | '_ ` _ \\ / _ \\ | | | \\ \\ / / _ \\ '__| |",
                "| |_| | (_| | | | | | |  __/ | |_| |\\ V /  __/ |  |_|",
                " \\____|\\__,_|_| |_| |_|\\___|  \\___/  \\_/ \\___|_|  (_)"
        };

        readonly float returnTimer = 5f;
        float timer;
        public override void Init()
        {
            GameManager.Instance.watch.Restart();
            GameManager.Instance.State = GameManager.EGameState.GameOver;
            //SceneManager.Instance.LoadScene(EType.Main);
        }

        public override void Render()
        {
            int centerX = ScreenBuffer.currentW / 2;
            ScreenBuffer.Draw(centerX - (skelton.Max(line => line.Length) / 2), 5, skelton,ConsoleColor.Red);
            ScreenBuffer.Draw(centerX - (gameOver.Max(line => line.Length) / 2), skelton.Length + 5, gameOver, ConsoleColor.Red);

            ScreenBuffer.Draw(centerX - (gameOver.Length)-9, skelton.Length + 12, $"Main screen after {timer:0.0} seconds.");
        }

        public override void Update()
        {
            timer = returnTimer - GameManager.Instance.watch.ElapsedMilliseconds/1000f;
            if (timer < 0) SceneManager.Instance.LoadScene(EType.Main);
        }
    }
}
