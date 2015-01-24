using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuestTheNumber
{
    public partial class _Default : Page
    {
        public const string UserName = "UserName";
        private const string Owner = "OwnerUserName";
        private const string Winner = "WinnerUserName";
        private const string ThinkedNumber = "ThinkedNumber";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack || Request[UserName] == null)
                return;

            Session.Add(UserName, Request[UserName]);
            Application.Add(Session[UserName].ToString(), new List<int>());
            ShowGameField();
        }

        private void ShowGameField()
        {
            LogIn.Visible = false;
            LogOff.Visible = true;
            GameBoardPanel.Visible = true;
            UIControlsPanel.Visible = true;
        }

        private void HideGameField()
        {
            LogIn.Visible = true;
            LogOff.Visible = false;
            GameBoardPanel.Visible = false;
            UIControlsPanel.Visible = false;
        }

        protected void LogInButton_OnClick(object sender, EventArgs e)
        {
            var user = UserNameTextBox.Text.Trim();
            if (user == string.Empty)
            {
                UserNameValidationLabel.Visible = true;
                return;
            }

            UserNameValidationLabel.Visible = false;

            var cookie = new HttpCookie(UserName)
            {
                Value = user,
                Expires = DateTime.Now.AddMinutes(20)
            };

            Response.Cookies.Add(cookie);
            Session.Add(UserName, user);
            Application.Add(Session[UserName].ToString(), new List<int>());

            ShowGameField();
        }

        protected void LogOutButton_OnClick(object sender, EventArgs e)
        {
            var cookie = new HttpCookie(UserName)
            {
                Expires = DateTime.Now.AddDays(-1)
            };
            Response.Cookies.Add(cookie);
            Session.Remove(UserName);

            HideGameField();
        }

        protected void GameBoard_OnPreRender(object sender, EventArgs e)
        {
            if (Application[Winner] != null)
            {
                GameStatusLabel.Text = string.Format("GG. Winner - {0}. Thinked number - {1}",
                    Application[Winner], Application[ThinkedNumber]);
            }
            else if (Application[Owner] != null)
            {
                GameStatusLabel.Text = string.Format("{0} start game.", Application[Owner]);
            }
            else
            {
                GameStatusLabel.Text = "> Let's start game <";
            }
        }

        protected void ThinkUpNumberButton_OnClick(object sender, EventArgs e)
        {
            var number = ThinkUpNumberTextBox.Text.Trim();
            if (!IsNumber(number))
            {
                ThinkUpNumberValidationLabel.Visible = true;
            }
            else
            {
                ThinkUpNumberValidationLabel.Visible = false;
                
                Application[Winner] = null;
                Application[Owner] = Session[UserName];
                Application[ThinkedNumber] = int.Parse(number);
            }
        }

        protected void GuessNumberButton_OnClick(object sender, EventArgs e)
        {
            if (Application[Owner] == Session[UserName])
            {
                HelperLabel.Text = "You can't play, cuz you started it!";
                return;
            }

            if (Application[ThinkedNumber] == null || Application[Winner] != null) 
                return;

            var numberStr = GuessNumberTextBox.Text.Trim();
            if (!IsNumber(numberStr))
            {
                GuessNumberValidationLabel.Visible = true;
                return;
            }
            GuessNumberValidationLabel.Visible = false;

            var thinkedNumber = int.Parse(Application[ThinkedNumber].ToString());
            var number = int.Parse(numberStr);

            ((List<int>)Application[Session[UserName].ToString()]).Add(number);

            if (number == thinkedNumber)
            {
                Application[Winner] = Session[UserName];
                HelperLabel.Text = String.Empty;
            }
            else if (number > thinkedNumber)
            {
                HelperLabel.Text = "Thinked number less than your!";
            }
            else
            {
                HelperLabel.Text = "Thinked number bigger than your!";
            }

            ShowHistory();
        }

        private void ShowHistory()
        {
            HistoryLabel.Text = "Your attempts : ";

            foreach (var number in (List<int>)Application[Session[UserName].ToString()])
            {
                HistoryLabel.Text += string.Format("{0}, ", number);
            }

            HistoryLabel.Text = HistoryLabel.Text.Substring(0, HistoryLabel.Text.Length - 2);
        }

        private bool IsNumber(string number)
        {
            return number != string.Empty || Regex.IsMatch(number, @"^(0)$|^([1-9][0-9]*)$");
        }
    }
}