using System.ComponentModel;
using System.Windows.Forms;

namespace Dynamo.Forms
{
    class BindableToolStripMenuItem : ToolStripMenuItem, IBindableComponent
    {
        #region IBindableComponent Members

        [Browsable(false)]
        public BindingContext BindingContext
        {
            get
            {
                _bindingContext = _bindingContext ?? new BindingContext();

                return _bindingContext;
            }
            set { _bindingContext = value; }
        }
        private BindingContext _bindingContext;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBindingsCollection DataBindings
        {
            get
            {
                _dataBindings = _dataBindings ?? new ControlBindingsCollection(this);

                return _dataBindings;
            }
        }
        private ControlBindingsCollection _dataBindings;

        #endregion
    }
}
