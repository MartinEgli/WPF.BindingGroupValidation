namespace WpfApp3
{
    public class EmployeeViewModel : ModelBase
    {
        private bool _isDirty;

        public EmployeeViewModel()
        {
            this.LoadData();
        }

        public EmployeeModel Employee { get; set; }

        public bool IsDirty
        {
            get
            {
                return this._isDirty;
            }
            set
            {
                if (this._isDirty == value) return;
                this._isDirty = value;
                this.OnChanged(nameof(this.IsDirty));
            }
        }

        private void LoadData()
        {
            //Employee = (from e in _context.Employee
            //            where e.Nr == 158
            //            select e).FirstOrDefault();

            this.Employee = new EmployeeModel { Firstname = "Billy", Lastname = "Wilder" };
        }

        public void Save()
        {
            //_context.SaveChanges();
        }
    }
}