using Analogy.Interfaces;
using System.Windows.Forms;

namespace Analogy.LogViewer.Example.IAnalogy
{
    public partial class UserControlExtensionExample : UserControl
    {
        public UserControlExtensionExample()
        {
            InitializeComponent();
        }

        public void UserClickMessage(IAnalogyLogMessage msg) => lblMsg.Text = msg.Text;
    }
}
