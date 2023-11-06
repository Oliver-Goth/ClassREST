namespace ClassREST.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BirthYear {get; set; }

        public void ValidateName()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                throw new ArgumentNullException("Name");
            }
            if (Name.Length < 0)
            {
                throw new ArgumentOutOfRangeException("Dit navn skal være mere end 0 karaktere langt, IDIOT");
            }
        }

        public void ValidateBirthYear()
        {
            if (BirthYear < 1820)
            {
                throw new ArgumentOutOfRangeException("Birthyear skal være 1820 eller højere, IDIOT");
            }
        }

        public void Validate()
        {
            ValidateName();
            ValidateBirthYear();
        }
    }
}
