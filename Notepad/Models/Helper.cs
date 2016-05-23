using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Notepad.Models
{
    public class Helper
    {
        //public static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data","dbNotepad.json");

        /// <summary>
        /// Обновление содержания блокнота
        /// </summary>
        /// <param name="newNotepadModel">ModelBinder блокнота</param>
        /// <returns></returns>
        public static bool UpdateContent(NewNotepadModel newNotepadModel)
        {
            var notepad = Database.LoadNotepads().FirstOrDefault(item => item.Name == newNotepadModel.Name);

            if (notepad==null)
            {
                return false;
            }

            notepad.Content = newNotepadModel.Content;

            return Database.UpdateNotepad(notepad);
        }

        /// <summary>
        /// Получить содержание блокнота
        /// </summary>
        /// <param name="name">Название блокнота</param>
        /// <returns></returns>
        public static string GetContent(string name)
        {
            var notepad = Database.LoadNotepads().FirstOrDefault(item => item.Name == name);

            if (notepad == null)
            {
                return "ERROR";
            }
            return notepad.Content ?? "";
        }

        /// <summary>
        /// Создать новый блокнот
        /// </summary>
        /// <param name="name">Название блокнота</param>
        /// <returns></returns>
        public static bool AddNotepad(string name)
        {
            if (Database.LoadNotepads().Any(notepad => notepad.Name == name))
            {
                return false;
            }

            StructureNotepad sn = new StructureNotepad();
            sn.Name = name;

            return Database.AddNotepad(name);
        }

        /// <summary>
        /// Сделать картинку по тексту
        /// </summary>
        /// <param name="text">Текст</param>
        /// <returns></returns>
        public static Image ToImage(string text)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);

            var drawing = Graphics.FromImage(img);
            var font = new Font("Courier New", 15);
            var textColor = Color.DarkGreen;
            var backColor = Color.Beige;

            //measure the string to see how big the image needs to be
            var textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        /// <summary>
        /// Преобразовать картинку в поток
        /// </summary>
        /// <param name="image">Картинка</param>
        /// <returns></returns>
        public static Stream ToStream(Image image)
        {
            var stream = new MemoryStream();
            var format = ImageFormat.Png;

            image.Save(stream, format);
            stream.Position = 0;

            return stream;
        }
    }
}