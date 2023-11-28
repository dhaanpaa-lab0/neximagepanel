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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NEXImageControlPanel.UserControls
{
    /// <summary>
    /// Interaction logic for TextInputValue.xaml
    /// </summary>
    public partial class TextInputValue : UserControl
    {


        public string TextLabel
        {
            get { return (string)GetValue(TextLabelProperty); }
            set { SetValue(TextLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextLabelProperty =
            DependencyProperty.Register(nameof(TextLabel), typeof(string), typeof(TextInputValue), new PropertyMetadata(""));




        public object Value
        {
            get { return (object)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(object), typeof(TextInputValue), new PropertyMetadata(null));



        public TextInputValue()
        {
            InitializeComponent();
        }
    }
}
