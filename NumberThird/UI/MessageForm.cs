using System;
using System.Drawing;
using System.Windows.Forms;

namespace EthicalExploit.UI {
  public class MessageForm : Form {
    private string _message = "Default Message";

    public MessageForm() {
      FormBorderStyle = FormBorderStyle.None;
      WindowState = FormWindowState.Maximized;
      TopMost = true;
      BackColor = Color.Red;
      ForeColor = Color.White;
      Font = new Font("Arial", 48, Fonstyle.Bold);
      TextAlign = ContentAligment.MiddleCenter;
    }

    public void SetMessage(string message) {
      _message = message;
      Text = _message;
    }

    protected override void OnPaint (PaintEvenArgs e) {
      base.OnPaint(e);
      //Center the Text
      StingFormat stringFormat = new StringFormat();
      stringFormat.Aligment = StringAligment.Center;
      stringFormat.LineArgument = StringAligment.Center;

      e.Graphics.DrawString(_message, Font, new SolidBrush(ForeColor), ClientRectangel, stringFormat);
    }
  }
}
