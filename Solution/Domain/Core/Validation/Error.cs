namespace Domain.Core.Validation
{
    /// <summary>
    /// Describes the result of a validation of a potential change through a business service.
    /// </summary>
    public class Error : Notification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="memberName">Name of the memeber.</param>
        /// <param name="message">The message.</param>
        public Error(string memberName, string message)
        {
            Message = message;
            MemberName = memberName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public Error(string message)
        {
            Message = message;
        }

        /// <summary>
        /// Gets or sets the name of the member.
        /// </summary>
        /// <value>
        /// The name of the member.  May be null for general validation issues.
        /// </value>
        public string MemberName { get; set; }
    }
}
