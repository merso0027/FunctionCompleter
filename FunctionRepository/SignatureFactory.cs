namespace SignatureRepository
{
    public class SignatureFactory
    {
        /// <summary>
        /// Get singatures source service.
        /// </summary>
        /// <returns>Signatures service</returns>
        public ISignaturesService GetSignatureService() {
            return new FileSignatureService();
        }
    }
}
