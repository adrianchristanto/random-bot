using Discord;
using Discord.Commands;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class ImageManipulationService
    {
        [Summary("Get avatar image")]
        public async Task GetAvatarFromUrl(IUser user)
        {
            var fileName = user.AvatarId + ".png";
            if (File.Exists(@"Image\AvatarTemplate\" + fileName) == false)
            {
                var userImage = new Uri(user.GetAvatarUrl());
                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(userImage, @"Image\AvatarTemplate\" + fileName);
                }
            }
        }

        [Summary("Manipulate image")]
        public void ManipulateImage(string directory, string avatarId, int xCoor, int yCoor, string saveFile)
        {
            using (var image = System.Drawing.Image.FromFile(@"Image\" + directory))
            using (var avatarImage = System.Drawing.Image.FromFile(@"Image\AvatarTemplate\" + avatarId + ".png"))
            using (var imageGraphics = Graphics.FromImage(image))
            using (var avatarBrush = new TextureBrush(avatarImage))
            {
                var x = xCoor;
                var y = yCoor;
                avatarBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(avatarBrush, new Rectangle(new Point(x, y), new Size(avatarImage.Width, avatarImage.Height)));
                image.Save(@"Image\ToUpload\" + saveFile);
            }
        }
    }
}
