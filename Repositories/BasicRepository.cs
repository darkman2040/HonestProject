namespace HonestProject.Repositories {
    public class BasicRepository : IBasicRepository
    {
        public bool ValidSubmission {get; set;}

        public bool ErrorDetected {get; set;}

        public string ErrorMessage {get; set;}

        public BasicRepository() {
            ValidSubmission = false;
            ErrorDetected = false;
            ErrorMessage = string.Empty;
        }

        public void ValidationPassed()
        {
            this.ValidSubmission = true;
        }

        public void SetError(string errorMessage)
        {
            this.ErrorDetected = true;
            this.ErrorMessage = ErrorMessage;
        }
    }
}