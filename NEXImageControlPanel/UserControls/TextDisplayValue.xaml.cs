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
    /// Interaction logic for TextDisplayValue.xaml
    /// </summary>
    public partial class TextDisplayValue : UserControl
    {


        public string TextLabel
        {
            get => (string)GetValue(TextLabelProperty);
            set => SetValue(TextLabelProperty, value);
        }

        // Using a DependencyProperty as the backing store for TextLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextLabelProperty =
            DependencyProperty.Register(nameof(TextLabel), typeof(string), 
                typeof(TextDisplayValue), new PropertyMetadata(""));

        #region Value DP

        /// <summary>
        /// Gets or sets the Value which is being displayed
        /// </summary>
        public object Value
        {
            get => (object)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Identified the Label dependency property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(object),
                typeof(TextDisplayValue), new PropertyMetadata(null));

        #endregion
        public TextDisplayValue()
        {
            InitializeComponent();
            
        }
    }
}
