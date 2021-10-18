// Decompiled with JetBrains decompiler
// Type: ClientA.Form1
// Assembly: ClientA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 3B451F4F-9880-4E2E-93A0-D0E06CC88CAA
// Assembly location: D:\Demo\Em\ClientA.exe

using ClientA.Models;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientA
{
  public class Form1 : Form
  {
    private SignalRClient r = new SignalRClient("http://110.10.130.51:900");
    private Uri u = new Uri("http://110.10.130.51:5002/Emergency/EventStatus/NonCheckEventStatus");
    private IContainer components = (IContainer) null;
    private Button bA;
    private Label la;

    public Form1()
    {
      this.InitializeComponent();
      this.r.Start().ContinueWith((Action<Task>) (task =>
      {
        if (!task.IsFaulted)
          return;
        int num = (int) MessageBox.Show("Error", "An error occurred when trying to connect to SignalR: " + task.Exception.InnerExceptions[0].Message);
      }));
      new System.Threading.Timer(new TimerCallback(this.callback)).Change(0, 5000);
      string str;
      this.r.OnMessageReceived += (SignalRClient.MessageReceived) ((username, message) => str = username + ": " + message);
    }

    private void callback(object o)
    {
      try
      {
        if (!this.r.IsConnectedOrConnecting)
        {
          this.r.Start();
        }
        else
        {
          Result result = new Result();
          pushdata pushdata1 = new pushdata();
          string empty = string.Empty;
          ReturnEventManagement rr = JsonConvert.DeserializeObject<ReturnEventManagement>(Net.GetURI("http://110.10.130.51:5002/Emergency/EventStatus/NonCheckEventStatus"));
          if (rr != null)
          {
            if (4 > int.Parse(rr.GroupCode.Substring(3, 2)))
            {
              string str1 = Encoding.UTF8.GetString(Encoding.Default.GetBytes(rr.EventRemark));
              if (rr.GroupCode == "EE-01")
                str1 = "거수자 발생!!!";
              else if (rr.GroupCode == "EE-02")
                str1 = "`긴급`, 화재발생!";
              else if (rr.GroupCode == "EE-03")
                str1 = "`긴급`, SOS 호출";
              string str2 = str1;
              this.la.Text = str1;
              pushdata pushdata2 = new pushdata();
              pushdata2.IdxDate = DateTime.Now.Ticks.ToString();
              pushdata2.MyPhoneNumber = "Emergency";
              pushdata2.message = str2;
              pushdata2.TxtReceiver = rr.GroupCode;
              pushdata2.sendDate = DateTime.Now.ToString();
              pushdata2.TxtTitleMessage = "Emergency Message";
              result.Header = "Emergency";
              result.Body = pushdata2;
              this.SendData(JsonConvert.SerializeObject((object) result, Formatting.Indented), rr);
            }
            else if (rr.EventType == "EVT-14")
            {
              string str = Encoding.UTF8.GetString(Encoding.Default.GetBytes(rr.EventRemark));
              this.la.Text = str;
              pushdata pushdata3 = new pushdata();
              pushdata3.IdxDate = DateTime.Now.Ticks.ToString();
              pushdata3.MyPhoneNumber = "Emergency";
              pushdata3.message = str;
              pushdata3.TxtReceiver = rr.GroupCode;
              pushdata3.sendDate = DateTime.Now.ToString();
              pushdata3.TxtTitleMessage = "GEOFENCE Message";
              result.Header = "GEOFENCE";
              result.Body = pushdata3;
              this.SendData(JsonConvert.SerializeObject((object) result, Formatting.Indented), rr);
            }
          }
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message;
      }
    }

    private void bA_Click(object sender, EventArgs e)
    {
    }

    private void SendData(string data, ReturnEventManagement rr)
    {
      string str = string.Empty;
      try
      {
        this.r.SendMessage("Emergency", data);
      }
      catch (Exception ex)
      {
        str = ex.Message;
      }
      finally
      {
        if (str == string.Empty)
        {
          this.u = new Uri("http://110.10.130.51:5002/Emergency/EventStatus/IsSendOkupdateY");
          Net.PostURI(this.u, (object) new UpdateEventStatus()
          {
            EventId = rr.EventId,
            IsSendOk = "Y"
          });
        }
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
            this.bA = new Button();
            this.la = new Label();
            this.SuspendLayout();
            // 
            // Form1
            // 
            this.bA.Location = new Point(52, 133);
            this.bA.Name = "bA";
            this.bA.Size = new Size(75, 23);
            this.bA.TabIndex = 0;
            this.bA.Text = "button1";
            this.bA.UseVisualStyleBackColor = true;
            this.bA.Click += new EventHandler(this.bA_Click);

            this.la.AutoSize = true;
            this.la.Location = new Point(240, 103);
            this.la.Name = "la";
            this.la.Size = new Size(45, 15);
            this.la.TabIndex = 1;
            this.la.Text = "label1";
            this.AutoScaleDimensions = new SizeF(8f, 15f);
            this.AutoScaleMode = AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add((Control)this.la);
            this.Controls.Add((Control)this.bA);

            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
  }
}
