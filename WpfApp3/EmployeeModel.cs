namespace WpfApp3
{
    public class EmployeeModel : ModelBase
    {
        private string _firstname;

        private string _lastname;

        private int _nr;

        private bool _isDirty;

        public int Nr
        {
            get
            {
                return this._nr;
            }

            set
            {
                this._nr = value;
                this.OnChanged(nameof(this.Nr));
            }
        }

        public string Firstname
        {
            get
            {
                return this._firstname;
            }

            set
            {
                this._firstname = value;
                this.OnChanged(nameof(this.Firstname));
            }
        }

        public string Lastname
        {
            get
            {
                return this._lastname;
            }

            set
            {
                this._lastname = value;
                this.OnChanged(nameof(this.Lastname));
            }
        }

        public bool IsDirty
        {
            get
            {
                return this._isDirty;
            }

            set
            {
                this._isDirty = value;
                this.OnChanged(nameof(this._isDirty));
            }
        }
    }
}