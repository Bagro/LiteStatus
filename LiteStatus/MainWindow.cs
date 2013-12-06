using System;
using System.Timers;
using Gtk;
using LiteStatus.Services;

namespace LiteStatus
{
	public partial class MainWindow: Gtk.Window
	{
		private Timer _updateTimer;
		private bool _timerStarted;

		public MainWindow () : base (Gtk.WindowType.Toplevel)
		{
			Build ();
		}

		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		protected void OnFetchBtnClicked (object sender, EventArgs e)
		{
			if(_updateTimer == null)
			{
				_updateTimer = new Timer(300000);
				_updateTimer.Elapsed += new ElapsedEventHandler(UpdateTimerElapsed);
				_timerStarted = false;
			}
			SetupSettings ();
			if (_timerStarted) {
				_updateTimer.Stop ();
				_timerStarted = false;
			} else {
				_updateTimer.Start ();
				_timerStarted = true;
				UpdateBalance ();
			}

			ToggleElements ();
		}

		private void ToggleElements()
		{
			if (_timerStarted) {
				timerBtn.Label = "Stop";
			} else {
				timerBtn.Label = "Start";
			}
		}

		private void UpdateTimerElapsed (object sender, ElapsedEventArgs e)
		{
			UpdateBalance ();
		}

		private void UpdateBalance()
		{
			var service = new LiteUpdateService();
			var balance = service.GetBalance ();
			if (balance != null) {
				confirmedLabel.Text = balance.confirmed.ToString ();
				unconfirmedLabel.Text = balance.unconfirmed.ToString ();
				orphanedLabel.Text = balance.unconfirmed.ToString ();
			}
		}

		private void SetupSettings()
		{
			LiteSettings.ApiKey = apiEntry.Text;
			LiteSettings.Id = Convert.ToInt32(idEntry.Text);
		}
	}
}