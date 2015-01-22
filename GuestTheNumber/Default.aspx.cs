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
        
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack && Request[Parameters.UserName] != null)
//            {
//                Session.Add(Parameters.UserName, Request[Parameters.UserName]);
//                Application.Add(Session[Parameters.UserName].ToString(), new List<int>());
//
//
//                HelloUserLabel.Text = Request[Parameters.UserName];
//                LogInControlsHide();
//            }
//            else
//            {
//                LogInControlsShow();
//            }
//        }
//
//        protected void LogInButton_Click(object sender, EventArgs e)
//        {
//            var userName = UserNameTextBox.Text.Trim();
//
//            if (userName == string.Empty)
//            {
//                UserNameValidationLabel.Visible = true;
//                return;
//            }
//
//            var cookie = new HttpCookie(Parameters.UserName)
//            {
//                Value = userName,
//                Expires = DateTime.Now.AddMinutes(20)
//            };
//            Response.Cookies.Add(cookie);
//            Session.Add(Parameters.UserName, userName);
//            Application.Add(Session[Parameters.UserName].ToString(), new List<int>());
//
//
//            LogInControlsHide();
//            HelloUserLabel.Text = string.Format("Hi, {0}!", userName);
//        }
//
//        protected void LogOutButton_Click(object sender, EventArgs e)
//        {
//            if (Request.Cookies[Parameters.UserName] != null)
//            {
//                var cookie = new HttpCookie(Parameters.UserName)
//                {
//                    Expires = DateTime.Now.AddDays(-1d)
//                };
//                Response.Cookies.Add(cookie);
//                Session.Remove(Parameters.UserName);
//            }
//
//            LogInControlsShow();
//        }
//
//        private void LogInControlsHide()
//        {
//            UserNameTextBox.Visible = false;
//            LogInButton.Visible = false;
//            UserNameValidationLabel.Visible = false;
//            LogOutButton.Visible = true;
//            HelloUserLabel.Visible = true;
//            GameBoardPanel.Visible = true;
//            UIControlsPanel.Visible = true;
//        }
//
//        private void LogInControlsShow()
//        {
//            UserNameTextBox.Visible = true;
//            LogInButton.Visible = true;
//            UserNameValidationLabel.Visible = false;
//            LogOutButton.Visible = false;
//            HelloUserLabel.Visible = false;
//            GameBoardPanel.Visible = false;
//            UIControlsPanel.Visible = false;
//        }
//
//        protected void GameBoard_PreRender(object sender, EventArgs e)
//        {
//            if (Application[Parameters.Winner] != null)
//            {
//                GameStatusLabel.Text = string.Format("GG. Winner - {0}. Thinked number - {1}",
//                    Application[Parameters.Winner], Application[Parameters.ThinkedNumber]);
//            }
//            if (Application[Parameters.Owner] != null)
//            {
//                GameStatusLabel.Text = string.Format("{0} start game.",Application[Parameters.Owner]);
//            }
//            else
//            {
//                GameStatusLabel.Text = "> Let's start game <";
//            }
//
//            gbLabel.Text = DateTime.Now.ToLongTimeString();
//            regLabel.Text = DateTime.Now.ToLongTimeString();
//            uiLabel.Text = DateTime.Now.ToLongTimeString();
//        }
//
//        protected void GuessNumberButton_Click(object sender, EventArgs e)
//        {
//            if (Application[Parameters.Owner] == Session[Parameters.UserName])
//            {
//                HelperLabel.Text = "Not for you!";
//                return;
//            }
//            if (Application[Parameters.Winner] != null)
//            {
//                HelperLabel.Text = "Too late!";
//                return;
//            }
//
//            var number = GuessNumberTextBox.Text.Trim();
//            if (!IsNumber(number))
//            {
//                GuessNumberValidationLabel.Visible = true;
//                return;
//            }
//
//            var thinkedNumber = (int)Application[Parameters.ThinkedNumber];
//            var numberInt = int.Parse(number);
//
//            ((List<int>) Application[Session[Parameters.UserName].ToString()]).Add(numberInt);
//
//            if (thinkedNumber == numberInt)
//            {
//                Application[Parameters.Winner] = Session[Parameters.UserName];
//                HelperLabel.Text = string.Empty;
//            }
//            else if (numberInt > thinkedNumber)
//            {
//                HelperLabel.Text = "Thinked number less than your!";
//            }
//            else
//            {
//                HelperLabel.Text = "Thinked number bigger than your!";
//            }
//
//            ShowHistory();
//            GuessNumberValidationLabel.Visible = false;
//        }
//
//
//
//        protected void ThinkUpNumberButton_Click(object sender, EventArgs e)
//        {
//            var number = ThinkUpNumberTextBox.Text.Trim();
//            if (!IsNumber(number))
//            {
//                ThinkUpNumberValidationLabel.Visible = true;
//            }
//            else
//            {
//                Application[Parameters.Winner] = null;
//                Application[Parameters.Owner] = Session[Parameters.UserName];
//                Application[Parameters.ThinkedNumber] = int.Parse(number);
//
//                ThinkUpNumberValidationLabel.Visible = false;
//            }
//
//            if (Application[Parameters.Owner] != null)
//            {
//                GameStatusLabel.Text = Application[Parameters.Owner] + "start game";
//                GameBoardPanel.Update();
//            }
//        }
//
//        private void ShowHistory()
//        {
//            HistoryLabel.Text = "Your attempts : ";
//            foreach (var number in (List<int>)Application[Session[Parameters.UserName].ToString()])
//            {
//                HistoryLabel.Text += string.Format("{0}, ", number);
//            }
//
//            HistoryLabel.Text = HistoryLabel.Text.Substring(0, HistoryLabel.Text.Length - 2);
//        }
//
//        private bool IsNumber(string number)
//        {
//            return number != string.Empty || Regex.IsMatch(number, @"^(0)$|^([1-9][0-9]*)$");
//        }


        
        
        /*NEWPART*/


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