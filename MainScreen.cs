using Gtk;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopFileCreator
{
    public class MainScreen : Window
    {
        public List<string> values = new List<string>() { "Application", "Link", "Directory" };
        public ComboBoxText TypeCombo = new ComboBoxText();
        public Entry VersionEntry = new Entry("1.0");
        public Entry NameEntry = new Entry();
        public Entry CommentEntry = new Entry();
        public FileChooserButton IconChooser = new FileChooserButton("Select Icon", FileChooserAction.Open);
        public FileChooserButton ExecChooser = new FileChooserButton("Select Executable", FileChooserAction.Open);
        public FileChooserButton PathChooser = new FileChooserButton("Select Working Directory", FileChooserAction.SelectFolder);
        public ToggleButton TerminalToggle = new ToggleButton();
        public Entry KeywordsEntry = new Entry();
        public Entry URLEntry = new Entry();
        public MainScreen() : base("Desktop File Creator")
        {

            Grid grid = new Grid();
            this.Add(grid);
            this.Resizable = false;

            Label TypeLabel = new Label("Type:");
            Label VersionLabel = new Label("Version:");
            Label NameLabel = new Label("Name:");
            Label CommentLabel = new Label("Comment:");
            Label IconLabel = new Label("Icon");
            Label ExecLabel = new Label("Exec:");
            Label PathLabel = new Label("Path:");
            Label TerminalLabel = new Label("Terminal:");
            Label KeywordsLabel = new Label("Keywords:");
            Label URLLabel = new Label("URL:");

            grid.Add(TypeLabel);
            grid.Attach(VersionLabel, 0, 1, 1, 1);
            grid.Attach(NameLabel, 0, 2, 1, 1);
            grid.Attach(CommentLabel, 0, 3, 1, 1);
            grid.Attach(IconLabel, 0, 4, 1, 1);
            grid.Attach(ExecLabel, 0, 5, 1, 1);
            grid.Attach(PathLabel, 0, 6, 1, 1);
            grid.Attach(TerminalLabel, 0, 7, 1, 1);
            grid.Attach(KeywordsLabel, 0, 8, 1, 1);
            grid.Attach(URLLabel, 0, 9, 1, 1);

            for (int i = 0; i < values.Count; i++)
            {
                TypeCombo.AppendText(values[i]);
            }
            TypeCombo.Active = 0;

            grid.Attach(TypeCombo, 1, 0, 1, 1);
            grid.Attach(VersionEntry, 1, 1, 1, 1);
            grid.Attach(NameEntry, 1, 2, 1, 1);
            grid.Attach(CommentEntry, 1, 3, 1, 1);
            grid.Attach(IconChooser, 1, 4, 1, 1);
            grid.Attach(ExecChooser, 1, 5, 1, 1);
            grid.Attach(PathChooser, 1, 6, 1, 1);
            grid.Attach(TerminalToggle, 1, 7, 1, 1);
            grid.Attach(KeywordsEntry, 1, 8, 1, 1);
            grid.Attach(URLEntry, 1, 9, 1, 1);

            Button SaveButton = new Button("Save");
            grid.Attach(SaveButton, 0, 10, 2, 1);

            SaveButton.Clicked += new EventHandler(SaveClick);

            this.ShowAll();
        }

        private void SaveClick(object obj, EventArgs args)
        {
            StringBuilder contents = new StringBuilder();
            contents.AppendLine("[Desktop Entry]");
            contents.AppendLine("Type=" + values[TypeCombo.Active]);
            if (VersionEntry.Text != "")
            {
                contents.AppendLine("Version=" + VersionEntry.Text);
            }
            if (NameEntry.Text != "")
            {
                contents.AppendLine("Name=" + NameEntry.Text);
            }

            if (CommentEntry.Text != "")
            {
                contents.AppendLine("Comment=" + CommentEntry.Text);
            }
            if (IconChooser.Filename != "" && IconChooser.Filename != null)
            {
                contents.AppendLine("Icon=" + IconChooser.Filename);
            }
            if (ExecChooser.Filename != "" && ExecChooser.Filename != null)
            {
                contents.AppendLine("Exec=" + ExecChooser.Filename);
            }
            if (PathChooser.Filename != "" && PathChooser.Filename != null)
            {
                contents.AppendLine("Path=" + PathChooser.Filename);
            }
            if (TerminalToggle.Mode)
            {
                contents.AppendLine("Terminal=true");
            }
            else
            {
                contents.AppendLine("Terminal=false");
            }
            if (KeywordsEntry.Text != "")
            {
                contents.AppendLine("Keywords=" + KeywordsEntry.Text);
            }
            if (URLEntry.Text != "")
            {
                contents.AppendLine("URL=" + URLEntry.Text);
            }

            Console.WriteLine(contents.ToString());
            FileChooserDialog saveFile = new FileChooserDialog("Save File", this, FileChooserAction.Save, "Cancel", ResponseType.Cancel,
            "Save", ResponseType.Accept);
            if (saveFile.Run() == (int)ResponseType.Accept)
            {
                try
                {
                    System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(saveFile.Filename);
                    streamWriter.Write(contents.ToString());
                    streamWriter.Close();
                    saveFile.Dispose();
                }
                catch (Exception e)
                {
                    saveFile.Dispose();
                    Console.WriteLine(e.Message + "\n" + e.StackTrace);

                    // Display Error
                    MessageDialog msg = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Error, ButtonsType.Ok, false, e.Message);
                    if (msg.Run() ==(int) ResponseType.Ok)
                    {
                        msg.Dispose();
                    }
                }
            }
            else { saveFile.Dispose(); }



        }
    }
}