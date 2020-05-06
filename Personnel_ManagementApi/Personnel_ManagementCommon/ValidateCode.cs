using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandMarkApply.Common
{
    /// <summary>
    /// 随机数和验证码
    /// </summary>
    public class ValidateCode
    {
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetValidateCode(int length)
        {
            string[] str = { "3", "4", "5", "6", "7", "8", "A", "B", "C", "D", "E", "F", "G", "H", "K", "M", "N", "P", "Q", "R", "S", "T", "W", "X", "Y" };
            StringBuilder validateCode = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                validateCode.Append(str[random.Next(str.Length)]);
            }
            return validateCode.ToString();
        }

        /// <summary>
        /// 图形验证码生成
        /// </summary>
        /// <param name="validateCode"></param>
        /// <returns></returns>
        public static byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 20.0), 40);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色(将背景色设置为白色)
                g.Clear(Color.White);
                //画图片的干扰色
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                Font font = new Font("Arial", 20, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                    Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
