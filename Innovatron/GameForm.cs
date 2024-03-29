﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Innovatron
{
    public partial class GameForm : Form
    {
        bool moveLeft, moveRight;
        int speed = 12;
        PictureBox interactionObjekt;
        Bitmap moveLeftPicture = new("..\\..\\..\\pictures\\moveLeft.png");
        Bitmap moveRightPicture = new("..\\..\\..\\pictures\\moveRight.png");
        string selectedAction;

        public GameForm()
        {
            InitializeComponent();
            ActionsList.Items.Add("cancel");
            ActionsList.Items.Add("take");
            ActionsList.Visible = false;
            button1.Visible = false;
            label1.Visible = false;
        }

        private void moveTimerEvent(object sender, EventArgs e)
        {
            if (moveLeft && player.Left > 0)
            {
                player.Left -= speed;
                if (player.Image != moveLeftPicture)
                {
                    player.Image = moveLeftPicture;
                }
            }
            if (moveRight && player.Left < 924)
            {
                player.Left += speed;
                if (player.Image != moveRightPicture)
                {
                    player.Image = moveRightPicture;
                }
            }
            if (interactionObjekt != null)
            {
                if (!player.Bounds.IntersectsWith(interactionObjekt.Bounds))
                {
                    interactionObjekt = null;
                }
            }
        }

        private void showActions(PictureBox _interactionObject)
        {
            interactionObjekt = _interactionObject;
            ActionsList.Visible = true;
            button1.Visible = true;
            label1.Visible = true;
            moveRight = false;
            moveLeft = false;
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            label2.Text = "";
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                moveLeft = true;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                moveRight = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                if (key.Bounds.IntersectsWith(player.Bounds))
                {
                    showActions(key);
                }
                if (door1.Bounds.IntersectsWith(player.Bounds))
                {
                    showActions(door1);
                }
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                moveLeft = false;
            }
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                moveRight = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectItemFromList();
        }

        private void KeyDownList(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                SelectItemFromList();
            }
        }

        private void SelectItemFromList()
        {
            ActionsList.Visible = false;
            button1.Visible = false;
            label1.Visible = false;
            selectedAction = (string)ActionsList.SelectedItem;

            ActionObject(key, "take", "open");
            Form newForm = new room2();
            Door(door1, newForm);
            //InformationObject(name, "text");
        }

        private void ActionObject(PictureBox objekt, string requiredAction, string getAction)
        {
            if (interactionObjekt == objekt && selectedAction == requiredAction)
            {
                ActionsList.Items.Add(getAction);
                key.Left = -100;
            }
            this.Focus();
        }

        private void InformationObject(PictureBox objekt, string text)
        {
            if (interactionObjekt == objekt && selectedAction == "read")
            {
                label2.Text = text;
            }
        }

        private void Door(PictureBox objekt, Form nextForm)
        {
            if (interactionObjekt == objekt && selectedAction == "open")
            {
                this.Hide();
                nextForm.Show();
            }
        }

        private void helpbutton_Click(object sender, EventArgs e)
        {
            GetHelpForm getHelpForm = new GetHelpForm();
            getHelpForm.ShowDialog();
        }
    }
}
