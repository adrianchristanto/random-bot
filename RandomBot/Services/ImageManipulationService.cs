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
        /// <summary>
        /// Get discord avatar image.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public Stream GetImageStream(string directory)
        {
            var image = Image.FromFile(directory);
            var rawFormat = this.GetImageFormat(image.RawFormat);
            return this.ToStream(image, rawFormat);
        }

        /// <summary>
        /// Manipulate image from existing template file.
        /// </summary>
        /// <param name="directory">Directory to template file.</param>
        /// <param name="avatarId">User's Avatar ID.</param>
        /// <param name="xCoor">X Coordinate for image manipulation.</param>
        /// <param name="yCoor">Y Coordinate for image manipulation.</param>
        /// <returns>Stream object.</returns>
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

        /// <summary>
        /// Manipulate image from existing stream file.
        /// </summary>
        /// <param name="stream">MemoryStream object.</param>
        /// <param name="avatarId">User's Avatar ID.</param>
        /// <param name="xCoor">X Coordinate for image manipulation.</param>
        /// <param name="yCoor">Y Coordinate for image manipulation.</param>
        /// <returns>Stream object.</returns>
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

        /// <summary>
        /// Get image extension format.
        /// </summary>
        /// <param name="rawFormat">ImageFormat from Image object.</param>
        /// <returns>ImageFormat.</returns>
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

        /// <summary>
        /// Change image format to Stream object.
        /// </summary>
        /// <param name="image">Image object.</param>
        /// <param name="format">Image format.</param>
        /// <returns>Stream object of image.</returns>
        private Stream ToStream(Image image, ImageFormat format)
        {
            var stream = new MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
    }
}
