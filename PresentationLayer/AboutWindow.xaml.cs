using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }


        private void tabconAbout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabAbout.IsSelected)
            {
                lblTitle.Content = "PixelBuilder v.2.0";
                txtblkText.Text = "PixelBuilder is a project made by Mads Rhea " +
                    "who loves making things with perler beads but can never decide what. " +
                    "Here, the userbase can make and share their creations with other users " +
                    "and save creations they like. Along with that, all of the guesswork of your " +
                    "typical perler bead making adventure is no more! The program will tell " +
                    "you exactly how many beads of what color by name you'll need to make" +
                    " a certain piece of art. Worried you won't get your color to match closely" +
                    " enough? No worries! All HEX values for the colors within this program" +
                    " match existing perler beads as closely as possible (provided by reddit " +
                    "user u/BIORIO) and each color is labeled. So, after making your beautiful " +
                    "piece of art, there's no guessing at all! All you have to worry about is having fun! :)";
            }

            if (tabFAQ.IsSelected)
            {
                lblTitle.Content = "Frequently Asked Questions";
                txtblkText.Text = 
                    "Q: Do you ever plan on implementing templates?" +
                    "\n\nA: Absolutely, I do! Right now with everything going on, it's not exactly feasible " +
                    "in the time limit I have but it's absolutely something I want to / plan on adding in " +
                    "sooner rather than later. Easier just to have a circle template than try and make it from scratch, yeah?" +
                    "\n\nQ: Would you ever be down to adding more bead colors / other brands?\n\n " +
                    "A: If you can provide me the HEX codes, certainly! It shouldn't be too difficult to add them in as the one man crew "
                    + "I'm operating at at the moment, but I can't foresee the future and maybe this will take off! " +
                    "If so, new colors will probably come in BIG batches instead of just a lot of little ones. TL;DR for now, yes."
                    + "\n\nQ: How did you make this?\n\n" +
                    "A: Sweat, tears, and an unrelenting love of crafts. Also, doing everything I can to not actually make the crafts. " +
                    "And this was something that got in the way of that.";
            }

            if (tabRules.IsSelected)
            {
                lblTitle.Content = "Rules ::";
                txtblkText.Text = "While you don't have to post online everything you make " +
                    "(at least, if you don't wanna save it), I can't stop you from making whatever. But," +
                    "if you want to post it to the community, the pattern has to go through clearance -" +
                    " which is done exclusively by the creator (me). I am the judge, jury, and executioner here.\n\n" +
                    "That's really the only rule. Don't post gross shit and don't name the stuff you make weird things either." +
                    " But, it also really doesn't matter if you do because I'll just delete it before anyone else sees it and " +
                    "I've seen enough online that I don't think a 50x50 drawing is gonna do much to my psyche.";
            }

            if (tabBugs.IsSelected)
            {
                lblTitle.Content = "Bugs ::";
                txtblkText.Text = "If you happen to run into any problems or find any bugs while using this program, " +
                    "please feel free to make a note of it at the link below and I'll try my best to resolve it.\n\nCurrent list of issues can be seen at :: \n https://www.github.com/madsrhea/pixelbuilder/issues";
            }
        }
    }
}
