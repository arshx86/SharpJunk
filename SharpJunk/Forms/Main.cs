#region

using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

#endregion


namespace SharpJunk.Forms;

public partial class Main : Form
{

    private static readonly Random rnd = new();

    public Main()
    {
        InitializeComponent();
    }

    private void Start(object sender, EventArgs e)
    {
        #region Prepare

        guna2Button1.Text = "Please Wait";

        int thread;

        if (string.IsNullOrWhiteSpace(threadBox.Text))
        {
            thread = 1; // default
        }
        else
        {
            if (!int.TryParse(threadBox.Text, out thread))
            {
                MessageBox.Show("Failed to parse thread count. Make sure thread count is a integer.", "Wrong Format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        var junk = new StringBuilder();

        string generatedRandomClass = Util.RandomClass();
        bool isStatic = generatedRandomClass.Contains("static");
        string addStatic = isStatic ? "static " : "";

        int randomNumber = rnd.Next(597814, 8175914);

        #endregion

        // Generate Usings

        for (var i = 0; i < thread; i++) junk.AppendLine(Util.RandomUsing());

        junk.AppendLine("using System;");
        junk.AppendLine("using System.Runtime.InteropServices;");

        // Add Namespace

        string body =
                $"\n\n\nnamespace {Util.RandomStr()} \n" + "{\n";
        junk.AppendLine(body);

        // Generate Class

        junk.AppendLine($"{generatedRandomClass} " + Util.RandomStr() + "\n{\n ");

        // Generate Array

        if (arraya.Checked)
        {
            for (var i = 0; i < thread; i++) junk.AppendLine($"{addStatic} {Util.AddArray()}");

            junk.AppendLine("\n\n");
        }

        // Generate DLL Imports

        if (dlla.Checked)
        {
            for (var i = 0; i < thread; i++)
            {
                junk.AppendLine($"[DllImport(\"{Util.RandomStr()}.dll\")]" + "\n");

                junk.AppendLine(
                        $"static extern {Util.RandomVariable()} {Util.RandomStr()}(System.IntPtr {Util.RandomStr()});");
            }

            junk.AppendLine("\n\n");
        }

        // Null Variables

        if (variablea.Checked)
        {
            for (var i = 0; i < thread; i++) junk.AppendLine($"{addStatic}{Util.RandomVariable(true)}{Util.RandomStr()} = null;");

            junk.AppendLine("\n");
        }

        // Methods

        if (methoda.Checked)
        {
            for (var i = 0; i < thread; i++)
            {
                string random = Util.RandomVariable();
                junk.AppendLine(addStatic + random + Util.RandomStr() + "()" + "\n{");

                // If else

                if (!ifelse.Checked) continue;
                var resultValue = $"{random}.Parse(\"{Util.RandomStr()}\");";

                junk.AppendLine(
                        $"if ({rnd.Next(3511581)} {Util.RandomOperator()} {rnd.Next(9468941)}) {{\n{Util.RandomVariable(true)} {Util.RandomStr()} = null; \n}}");

                junk.AppendLine(
                        $"else if ({rnd.Next(538991)} {Util.RandomOperator()} {rnd.Next(938991)}) {{\n{Util.RandomVariable(true)} {Util.RandomStr()} = null; \n}}");
                junk.AppendLine($"return {resultValue}\n}}"); // last return
            }

            junk.AppendLine("\n\n");
        }

        // Functions

        if (funcs.Checked)
        {
            for (var i = 0; i < thread; i++)
            {
                string random = Util.RandomStuff();
                string ıfIsDelegate = random.Contains("dele") ? "" + random : addStatic + " " + random;
                junk.AppendLine($"{ıfIsDelegate}"); // add new stuff
            }

            junk.AppendLine("\n\n");
        }

        // Enums

        if (enuma.Checked)
        {
            junk.AppendLine($"enum {Util.RandomStr()} \n" + "{");

            for (var i = 0; i < thread; i++) junk.AppendLine($"{Util.RandomStr()} = 0x{randomNumber}, \n "); // add new enum

            junk.AppendLine("}\n\n");
        }

        // Flush

        junk.AppendLine("}");
        junk.AppendLine("\n}");

        string formatted = ArrangeUsingRoslyn(junk.ToString()); // Format the text

        guna2Button1.Text = "Generate";
        richTextBox1.Text = formatted; // Cast to textbox
    }

    private void guna2HtmlLabel5_Click(object sender, EventArgs e)
    {
    }

    private void usinga_CheckedChanged(object sender, EventArgs e)
    {
    }

    #region Modules

    private void ColorTextBox(object sender, EventArgs e)
    {
        CheckKeyword("while", Color.Purple);
        CheckKeyword("if", Color.Green);
        CheckKeyword("static", Color.Aqua);
        CheckKeyword("long", Color.MediumVioletRed);
        CheckKeyword("short", Color.MediumVioletRed);
        CheckKeyword("bool", Color.MediumVioletRed);
        CheckKeyword("int", Color.MediumVioletRed);
        CheckKeyword("byte", Color.MediumVioletRed);
        CheckKeyword("string", Color.MediumVioletRed);
        CheckKeyword("double", Color.MediumVioletRed);
        CheckKeyword("decimal", Color.MediumVioletRed);
        CheckKeyword("float", Color.MediumVioletRed);
        CheckKeyword("null", Color.DarkOrange);
        CheckKeyword("class", Color.Aqua);
        CheckKeyword("namespace", Color.Aqua);
        CheckKeyword("using", Color.Chartreuse);
        CheckKeyword("enum", Color.Red);
        CheckKeyword("=", Color.Red);
    }

    private void CheckKeyword(string word, Color color, int startIndex = 0)
    {
        var Rchtxt = richTextBox1;

        if (!Rchtxt.Text.Contains(word))
        {
            return;
        }

        int index = -1;
        int selectStart = Rchtxt.SelectionStart;

        while ((index = Rchtxt.Text.IndexOf(word, index + 1, StringComparison.Ordinal)) != -1)
        {
            Rchtxt.Select(index + startIndex, word.Length);
            Rchtxt.SelectionColor = color;
            Rchtxt.Select(selectStart, 0);
            Rchtxt.SelectionColor = Color.Black;
        }
    }

    private static string ArrangeUsingRoslyn(string csCode)
    {
        var tree = CSharpSyntaxTree.ParseText(csCode);
        var root = tree.GetRoot().NormalizeWhitespace();
        string ret = root.ToFullString();
        return ret;
    }

    #endregion

    #region Events

    private void Form1_Load(object sender, EventArgs e)
    {
    }

    private void guna2ControlBox1_Click(object sender, EventArgs e)
    {
        Environment.Exit(0);
    }

    private void Settingsbtn_Click(object sender, EventArgs e)
    {
        panelSet.Visible = true;
        panelGen.Visible = false;
        infoPanel.Visible = false;
    }

    private void Genbtn_Click(object sender, EventArgs e)
    {
        panelSet.Visible = false;
        panelGen.Visible = true;
        infoPanel.Visible = false;
    }

    private void Infobtn_Click(object sender, EventArgs e)
    {
        panelSet.Visible = false;
        panelGen.Visible = false;
        infoPanel.Visible = true;
    }

    private void guna2Button3_Click(object sender, EventArgs e)
    {
        Process.Start("https://discordapp.com/users/551357127210303525");
    }

    private void guna2Button2_Click_1(object sender, EventArgs e)
    {
        Process.Start("https://github.com/arshx86");
    }

    #endregion

}