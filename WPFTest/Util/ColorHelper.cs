using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace WPFTest.Util
{
    /// <summary>
    /// Image Color Helper
    /// </summary>
    internal class ColorHelper
    {
        #region GetTByKey

        public static T GetResourceByKey<T>(string key)
        {
            try
            {
                object resource = Application.Current.FindResource(key);
                return (T)resource;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw new ResourceReferenceKeyNotFoundException(key, ex);
            }
        }

        public static ImageSource GetResourceByKey(string key)
        {
            try
            {
                DrawingImage drawingImage = GetResourceByKey<DrawingImage>(key);
                ImageSource imageSource = DrawingImageToImageSource(drawingImage);
                return imageSource;
            }
            catch (Exception ex)
            {
                throw new Exception(key, ex);
            }
        }
        #endregion

        #region GetDominantColor

        public static string GetDominantColorStr(string key)
        {
            try
            {
                var dominantColor = GetDominantColor(key);

                var color = $"{dominantColor.R}, {dominantColor.G}, {dominantColor.B}";
                return color;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }
        public static Brush GetDominantColorBrush(string key)
        {
            try
            {
                System.Drawing.Color dominantColor = GetDominantColor(key);
                System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromArgb(dominantColor.A, dominantColor.R, dominantColor.G, dominantColor.B);
                System.Windows.Media.Brush brush = new System.Windows.Media.SolidColorBrush(mediaColor);

                return brush;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }
        public static Brush GetDominantColorBrush(Image image)
        {
            try
            {
                var bitmapSource = ConvertToBitmapSource(image);
                System.Drawing.Color dominantColor = GetDominantColor(bitmapSource);
                return ConverterColorToBrush(dominantColor);
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }
        public static string GetDominantColorStr(DrawingImage drawingImage)
        {
            try
            {
                System.Drawing.Color dominantColor = GetDominantColor(drawingImage);
                string hexColor = ColorTranslator.ToHtml(dominantColor);
                return hexColor;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }
        public static Brush GetDominantColorBrush(DrawingImage drawingImage)
        {
            try
            {
                System.Drawing.Color dominantColor = GetDominantColor(drawingImage);
                System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromArgb(dominantColor.A, dominantColor.R, dominantColor.G, dominantColor.B);
                Brush brush = new System.Windows.Media.SolidColorBrush(mediaColor);
                return brush;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }
        public static Brush GetDominantColorBrush(DrawingImage drawingImage, int index)
        {
            try
            {
                System.Drawing.Color dominantColor = GetDominantColor(drawingImage, index);
                System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromArgb(dominantColor.A, dominantColor.R, dominantColor.G, dominantColor.B);
                Brush brush = new System.Windows.Media.SolidColorBrush(mediaColor);
                return brush;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }
        public static Brush GetDominantColorBrush(ImageSource imageSource)
        {
            try
            {
                var bitmapSource = ConvertImageSourceToBitmapImage(imageSource);
                //SaveImage(bitmapSource);
                System.Drawing.Color dominantColor = GetDominantColor(bitmapSource);
                return ConverterColorToBrush(dominantColor);
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Converter

        private static Brush ConverterColorToBrush(System.Drawing.Color dominantColor)
        {
            System.Windows.Media.Color mediaColor = System.Windows.Media.Color.FromArgb(dominantColor.A, dominantColor.R, dominantColor.G, dominantColor.B);
            return new System.Windows.Media.SolidColorBrush(mediaColor);
        }


        /// <summary>
        /// DrawingImage => ImageSource
        /// </summary>
        /// <param name="drawingImage">需要转换的DrawingImage</param>
        /// <returns>转换后的ImageSource</returns>
        public static ImageSource DrawingImageToImageSource(DrawingImage drawingImage)
        {
            if (drawingImage == null)
            {
                return null;
            }

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawImage(drawingImage, new Rect(new System.Windows.Point(0, 0), new System.Windows.Size(drawingImage.Width, drawingImage.Height)));
            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)drawingImage.Width, (int)drawingImage.Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            return bmp;
        }

        /// <summary>
        /// DrawingImage => BitmapImage
        /// </summary>
        /// <param name="drawingImage"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static BitmapImage ConvertDrawingImageToBitmapImage(DrawingImage drawingImage)
        {
            if (drawingImage == null)
            {
                throw new ArgumentNullException("DrawingImage cannot be null.");
            }
            // RenderTargetBitmap对象，用于渲染DrawingImage
            RenderTargetBitmap renderTargetBitmap =
                new RenderTargetBitmap((int)drawingImage.Width, (int)drawingImage.Height, 96, 96, PixelFormats.Pbgra32);
            // 创建一个DrawingVisual对象，用于承载DrawingImage
            DrawingVisual drawingVisual = new DrawingVisual();
            // 获取DrawingVisual的绘图上下文
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                // 在绘图上下文中绘制DrawingImage
                drawingContext.DrawImage(drawingImage, new Rect(0, 0, drawingImage.Width, drawingImage.Height));
            }
            // 将DrawingVisual渲染到RenderTargetBitmap上
            renderTargetBitmap.Render(drawingVisual);
            // 创建一个PngBitmapEncoder对象，用于编码RenderTargetBitmap为PNG格式的字节流
            PngBitmapEncoder pngBitmapEncoder = new PngBitmapEncoder();
            pngBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
            // 创建一个MemoryStream对象，用于存储PNG格式的字节流
            MemoryStream memoryStream = new MemoryStream();
            // 将PngBitmapEncoder的内容写入MemoryStream
            pngBitmapEncoder.Save(memoryStream);
            // 创建一个BitmapImage对象，用于从MemoryStream中加载PNG格式的字节流
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
            // 返回BitmapImage对象
            return bitmapImage;
        }

        /// <summary>
        /// ImageSource => DrawingImage
        /// </summary>
        /// <param name="imageSource"></param>
        /// <returns></returns>
        public static BitmapSource ConvertImageSourceToBitmapImage(ImageSource imageSource)
        {
            if (imageSource is BitmapSource bitmapSource)
            {
                return bitmapSource;
            }

            var width = (int)Math.Round(imageSource.Width);
            var height = (int)Math.Round(imageSource.Height);
            var renderTarget = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            var visual = new DrawingVisual();
            using (var context = visual.RenderOpen())
            {
                context.DrawImage(imageSource, new Rect(0, 0, width, height));
            }
            renderTarget.Render(visual);
            return renderTarget;
        }

        #endregion

        #region SaveImage

        /// <summary>
        /// 将ImageSource保存到本地
        /// </summary>
        /// <param name="imageSource">需要保存的ImageSource</param>
        /// <param name="fileName">保存的文件名</param>
        public static void SaveImageSourceToFile(ImageSource imageSource)
        {
            if (imageSource == null) return;
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imageSource));
            string currentPath = Environment.CurrentDirectory;
            string currentEvent = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = Path.Combine(currentPath, "imageSource_" + currentEvent + ".png");
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        /// <summary>
        /// 根据Key获取DrawingImage存储为PNG
        /// </summary>
        /// <param name="key"></param>
        public static void SaveSourcetoPNG(string key)
        {
            DrawingImage drawingImage = GetResourceByKey<DrawingImage>(key);
            ImageSource imageSource = DrawingImageToImageSource(drawingImage);
            //SaveImageSourceToFile(imageSource);
        }

        /// <summary>
        /// BitmapSource 存储为PNG
        /// </summary>
        /// <param name="bitmapSource"></param>
        public static void SaveImage(BitmapSource bitmapSource)
        {
            string currentPath = Environment.CurrentDirectory;
            string currentEvent = DateTime.Now.ToString("yyyyMMddHHmmss");
            string imagePath = Path.Combine(currentPath, "image_" + currentEvent + ".png");
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }
        #endregion

        #region 私有方法
        private static System.Drawing.Color GetDominantColor(string key)
        {
            try
            {
                DrawingImage drawingImage = GetResourceByKey<DrawingImage>(key);
                BitmapSource bitmapSource = ConvertDrawingImageToBitmapImage(drawingImage);

                Dictionary<System.Drawing.Color, int> colorCounts = new Dictionary<System.Drawing.Color, int>();
                for (int x = 0; x < (int)bitmapSource.Width; x++)
                {
                    for (int y = 0; y < (int)bitmapSource.Height; y++)
                    {
                        var pixelBuffer = new byte[4];
                        //System.Drawing.Color pixelColor = bitmapSource.GetPixel(x, y);
                        bitmapSource.CopyPixels(new Int32Rect(x, y, 1, 1), pixelBuffer, 4, 0);
                        var pixelColor = System.Drawing.Color.FromArgb(pixelBuffer[3], pixelBuffer[2], pixelBuffer[1], pixelBuffer[0]);
                        if (pixelColor.A == 0) continue;
                        if (colorCounts.ContainsKey(pixelColor))
                        {
                            colorCounts[pixelColor]++;
                        }
                        else
                        {
                            colorCounts.Add(pixelColor, 1);
                        }
                    }
                }

                System.Drawing.Color dominantColor = colorCounts.OrderByDescending(c => c.Value).Select(c => c.Key).FirstOrDefault();

                return dominantColor;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }

        }
        private static System.Drawing.Color GetDominantColor(BitmapSource bitmapSource)
        {
            try
            {
                var colorCounts = new Dictionary<System.Drawing.Color, int>();
                for (int x = 0; x < (int)bitmapSource.Width; x++)
                {
                    for (int y = 0; y < (int)bitmapSource.Height; y++)
                    {
                        var pixelBuffer = new byte[4];
                        //System.Drawing.Color pixelColor = bitmapSource.GetPixel(x, y);
                        bitmapSource.CopyPixels(new Int32Rect(x, y, 1, 1), pixelBuffer, 4, 0);
                        var pixelColor = System.Drawing.Color.FromArgb(pixelBuffer[3], pixelBuffer[2], pixelBuffer[1], pixelBuffer[0]);
                        if (pixelColor.A == 0) continue;
                        if (colorCounts.ContainsKey(pixelColor))
                        {
                            colorCounts[pixelColor]++;
                        }
                        else
                        {
                            colorCounts.Add(pixelColor, 1);
                        }
                    }
                }

                System.Drawing.Color dominantColor = colorCounts.OrderByDescending(c => c.Value).Select(c => c.Key).FirstOrDefault();

                return dominantColor;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }

        }
        private static System.Drawing.Color GetDominantColor(DrawingImage drawingImage)
        {
            try
            {
                BitmapSource bitmapSource = ConvertDrawingImageToBitmapImage(drawingImage);

                Dictionary<System.Drawing.Color, int> colorCounts = new Dictionary<System.Drawing.Color, int>();
                for (int x = 0; x < (int)bitmapSource.Width; x++)
                {
                    for (int y = 0; y < (int)bitmapSource.Height; y++)
                    {
                        var pixelBuffer = new byte[4];
                        bitmapSource.CopyPixels(new Int32Rect(x, y, 1, 1), pixelBuffer, 4, 0);
                        var pixelColor = System.Drawing.Color.FromArgb(pixelBuffer[3], pixelBuffer[2], pixelBuffer[1], pixelBuffer[0]);
                        if (pixelColor.A == 0) continue;
                        if (colorCounts.ContainsKey(pixelColor))
                        {
                            colorCounts[pixelColor]++;
                        }
                        else
                        {
                            colorCounts.Add(pixelColor, 1);
                        }
                    }
                }

                System.Drawing.Color dominantColor = colorCounts.OrderByDescending(c => c.Value).Select(c => c.Key).FirstOrDefault();

                return dominantColor;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }
        private static System.Drawing.Color GetDominantColor(DrawingImage drawingImage, int rank)
        {
            try
            {
                BitmapSource bitmapSource = ConvertDrawingImageToBitmapImage(drawingImage);

                Dictionary<System.Drawing.Color, int> colorCounts = new Dictionary<System.Drawing.Color, int>();
                for (int x = 0; x < (int)bitmapSource.Width; x++)
                {
                    for (int y = 0; y < (int)bitmapSource.Height; y++)
                    {
                        var pixelBuffer = new byte[4];
                        bitmapSource.CopyPixels(new Int32Rect(x, y, 1, 1), pixelBuffer, 4, 0);
                        var pixelColor = System.Drawing.Color.FromArgb(pixelBuffer[3], pixelBuffer[2], pixelBuffer[1], pixelBuffer[0]);
                        if (pixelColor.A == 0) continue;
                        if (colorCounts.ContainsKey(pixelColor))
                        {
                            colorCounts[pixelColor]++;
                        }
                        else
                        {
                            colorCounts.Add(pixelColor, 1);
                        }
                    }
                }

                System.Drawing.Color dominantColor = colorCounts.OrderByDescending(c => c.Value).Skip(rank - 1).First().Key;

                return dominantColor;
            }
            catch (ResourceReferenceKeyNotFoundException ex)
            {
                throw ex;
            }
        }

        public static BitmapSource ConvertToBitmapSource(Image image)
        {
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)image.ActualWidth, (int)image.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            image.Measure(new System.Windows.Size((int)image.ActualWidth, (int)image.ActualHeight));
            image.Arrange(new System.Windows.Rect(new System.Windows.Size((int)image.ActualWidth, (int)image.ActualHeight)));
            bitmap.Render(image);
            return bitmap;
        }
        #endregion
    }
}
