using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RandomBot.Services
{
    public class ImageManipulationService
    {
        public async Task GetAvatarFromUrl(Discord.IUser user)
        {
            var fileName = $"{ user.AvatarId }.png";
            if (File.Exists($@"Image\AvatarTemplate\{ fileName }") == false && string.IsNullOrEmpty(user.AvatarId) == false)
            {
                var userImage = new Uri(user.GetAvatarUrl());
                using (var client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(userImage, $@"Image\AvatarTemplate\{ fileName }");
                }
            }
        }
        
        public Stream GetImageStream(string directory)
        {
            var image = Image.FromFile(directory);
            var rawFormat = this.GetImageFormat(image.RawFormat);
            return this.ToStream(image, rawFormat);
        }
        
        public Stream ManipulateImage(string directory, string avatarId, int xCoor, int yCoor)
        {
            avatarId = avatarId ?? "NoAvatar";

            using (var image = Image.FromFile($@"Image\{ directory }"))
            using (var avatarImage = Image.FromFile($@"Image\AvatarTemplate\{ avatarId }.png"))
            using (var imageGraphics = Graphics.FromImage(image))
            using (var avatarBrush = new TextureBrush(avatarImage))
            {
                var x = xCoor;
                var y = yCoor;
                avatarBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(avatarBrush, new Rectangle(new Point(x, y), new Size(avatarImage.Width, avatarImage.Height)));

                return ToStream(image, this.GetImageFormat(image.RawFormat));
            }
        }
        
        public Stream ManipulateImage(Stream stream, string avatarId, int xCoor, int yCoor)
        {
            avatarId = avatarId ?? "NoAvatar";
            var image = Image.FromStream(stream);

            using (var avatarImage = Image.FromFile($@"Image\AvatarTemplate\{ avatarId }.png"))
            using (var imageGraphics = Graphics.FromImage(image))
            using (var avatarBrush = new TextureBrush(avatarImage))
            {
                var x = xCoor;
                var y = yCoor;
                avatarBrush.TranslateTransform(x, y);
                imageGraphics.FillRectangle(avatarBrush, new Rectangle(new Point(x, y), new Size(avatarImage.Width, avatarImage.Height)));

                return ToStream(image, this.GetImageFormat(image.RawFormat));
            }
        }

        private ImageFormat GetImageFormat(ImageFormat rawFormat)
        {
            if (rawFormat.Equals(ImageFormat.Jpeg))
                return ImageFormat.Jpeg;
            if (rawFormat.Equals(ImageFormat.Bmp))
                return ImageFormat.Bmp;
            if (rawFormat.Equals(ImageFormat.Png))
                return ImageFormat.Png;
            if (rawFormat.Equals(ImageFormat.Emf))
                return ImageFormat.Emf;
            if (rawFormat.Equals(ImageFormat.Exif))
                return ImageFormat.Exif;
            if (rawFormat.Equals(ImageFormat.Gif))
                return ImageFormat.Gif;
            if (rawFormat.Equals(ImageFormat.Icon))
                return ImageFormat.Icon;
            if (rawFormat.Equals(ImageFormat.MemoryBmp))
                return ImageFormat.MemoryBmp;
            if (rawFormat.Equals(ImageFormat.Tiff))
                return ImageFormat.Tiff;
            else
                return ImageFormat.Wmf;
        }
        
        private Stream ToStream(Image image, ImageFormat format)
        {
            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
    }
}
