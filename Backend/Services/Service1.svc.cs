using EveryBeats.Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EveryBeats
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        bool IService1.addCollaboration(int beatID, int producerID, decimal splitPercent)
        {
            throw new NotImplementedException();
        }

        int IService1.addGenre(string genreName)
        {
            throw new NotImplementedException();
        }

        int IService1.addLicense(License newLicense)
        {
            throw new NotImplementedException();
        }

        bool IService1.addToCart(int userID, int beatID, string licenseType)
        {
            throw new NotImplementedException();
        }

        bool IService1.approvePayout(int payoutID)
        {
            throw new NotImplementedException();
        }

        bool IService1.clearCart(int userID)
        {
            throw new NotImplementedException();
        }

        bool IService1.completeOrder(int orderID)
        {
            throw new NotImplementedException();
        }

        int IService1.createOrder(int userID)
        {
            throw new NotImplementedException();
        }

        bool IService1.createPayout(int producerID, decimal amount)
        {
            throw new NotImplementedException();
        }

        bool IService1.createSplit(int orderItemID, int producerID, decimal percentage, decimal amount)
        {
            throw new NotImplementedException();
        }

        bool IService1.deactivateUser(int userID)
        {
            throw new NotImplementedException();
        }

        bool IService1.deleteBeat(int beatID)
        {
            throw new NotImplementedException();
        }

        bool IService1.deleteLicense(int licenseID)
        {
            throw new NotImplementedException();
        }

        int IService1.generateAgreement(int orderItemID, int producerID, int userID)
        {
            throw new NotImplementedException();
        }

        Agreement IService1.getAgreementByID(int agreementID)
        {
            throw new NotImplementedException();
        }

        Agreement IService1.getAgreementByOrderItem(int orderItemID)
        {
            throw new NotImplementedException();
        }

        string IService1.getAgreementPDFUrl(int agreementID)
        {
            throw new NotImplementedException();
        }

        List<Beat> IService1.getAllBeats()
        {
            throw new NotImplementedException();
        }

        List<Genre> IService1.getAllGenres()
        {
            throw new NotImplementedException();
        }

        List<User> IService1.getAllUsers()
        {
            throw new NotImplementedException();
        }

        decimal IService1.getAverageSale(int producerID)
        {
            throw new NotImplementedException();
        }

        Beat IService1.getBeatByID(int beatID)
        {
            throw new NotImplementedException();
        }

        List<Beat> IService1.getBeatsByGenre(int genreID)
        {
            throw new NotImplementedException();
        }

        List<Beat> IService1.getBeatsByProducer(int producerID)
        {
            throw new NotImplementedException();
        }

        List<ShoppingCart> IService1.getCart(int userID)
        {
            throw new NotImplementedException();
        }

        decimal IService1.getCartTotal(int userID)
        {
            throw new NotImplementedException();
        }

        List<Collaboration> IService1.getCollaborationsByBeat(int beatID)
        {
            throw new NotImplementedException();
        }

        string IService1.GetData(int value)
        {
            throw new NotImplementedException();
        }

        CompositeType IService1.GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        License IService1.getLicenseByID(int licenseID)
        {
            throw new NotImplementedException();
        }

        List<License> IService1.getLicensesByBeat(int beatID)
        {
            throw new NotImplementedException();
        }

        Order IService1.getOrderByID(int orderID)
        {
            throw new NotImplementedException();
        }

        List<Order> IService1.getOrdersByUser(int userID)
        {
            throw new NotImplementedException();
        }

        List<Payout> IService1.getPayoutsByProducer(int producerID)
        {
            throw new NotImplementedException();
        }

        List<OrderItem> IService1.getSalesHistory(int producerID)
        {
            throw new NotImplementedException();
        }

        List<Split> IService1.getSplitsByOrderItem(int orderItemID)
        {
            throw new NotImplementedException();
        }

        List<Split> IService1.getSplitsByProducer(int producerID)
        {
            throw new NotImplementedException();
        }

        string IService1.getTopBeatTitle(int producerID)
        {
            throw new NotImplementedException();
        }

        int IService1.getTotalBeats(int producerID)
        {
            throw new NotImplementedException();
        }

        decimal IService1.getTotalRevenue(int producerID)
        {
            throw new NotImplementedException();
        }

        int IService1.getTotalSales(int producerID)
        {
            throw new NotImplementedException();
        }

        User IService1.getUserByID(int userID)
        {
            throw new NotImplementedException();
        }

        bool IService1.isLicenseAvailable(int beatID, string licenseType)
        {
            throw new NotImplementedException();
        }

        bool IService1.loginUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        bool IService1.markPayoutPaid(int payoutID)
        {
            throw new NotImplementedException();
        }

        bool IService1.processPayment(int orderID, decimal amount, string paymentMethod)
        {
            throw new NotImplementedException();
        }

        bool IService1.refundOrder(int orderID)
        {
            throw new NotImplementedException();
        }

        int IService1.registerUser(User newUser)
        {
            throw new NotImplementedException();
        }

        bool IService1.removeCollaboration(int beatID, int producerID)
        {
            throw new NotImplementedException();
        }

        bool IService1.removeFromCart(int userID, int beatID)
        {
            throw new NotImplementedException();
        }

        List<Beat> IService1.searchBeats(string keyword)
        {
            throw new NotImplementedException();
        }

        bool IService1.updateBeat(Beat beat)
        {
            throw new NotImplementedException();
        }

        bool IService1.updateCollaboration(int beatID, int producerID, decimal splitPercent)
        {
            throw new NotImplementedException();
        }

        bool IService1.updateLicense(License license)
        {
            throw new NotImplementedException();
        }

        bool IService1.updateUserProfile(int userID, string firstName, string lastName, string cellnumber)
        {
            throw new NotImplementedException();
        }

        int IService1.uploadBeat(Beat newBeat)
        {
            throw new NotImplementedException();
        }

        bool IService1.validateSplits(int beatID)
        {
            throw new NotImplementedException();
        }
    }
}
