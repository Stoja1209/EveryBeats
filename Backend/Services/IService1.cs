using EveryBeats.Backend.Data;
using EveryBeats.Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace EveryBeats
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        // ============================================================
        // USER MANAGEMENT
        // ============================================================
        [OperationContract]
        bool loginUser(string email, string password);

        [OperationContract]
        int registerUser(User newUser);

        [OperationContract]
        bool updateUserProfile(int userID, string firstName, string lastName, string cellnumber);

        [OperationContract]
        bool deactivateUser(int userID);

        [OperationContract]
        User getUserByID(int userID);

        [OperationContract]
        List<User> getAllUsers();

        // ============================================================
        // BEAT MANAGEMENT
        // ============================================================
        [OperationContract]
        int uploadBeat(Beat newBeat);

        [OperationContract]
        bool updateBeat(Beat beat);

        [OperationContract]
        bool deleteBeat(int beatID);

        [OperationContract]
        Beat getBeatByID(int beatID);

        [OperationContract]
        List<Beat> getAllBeats();

        [OperationContract]
        List<Beat> getBeatsByProducer(int producerID);

        [OperationContract]
        List<Beat> getBeatsByGenre(int genreID);

        [OperationContract]
        List<Beat> searchBeats(string keyword);

        // ============================================================
        // LICENSE MANAGEMENT
        // ============================================================
        [OperationContract]
        int addLicense(License newLicense);

        [OperationContract]
        bool updateLicense(License license);

        [OperationContract]
        bool deleteLicense(int licenseID);

        [OperationContract]
        License getLicenseByID(int licenseID);

        [OperationContract]
        List<License> getLicensesByBeat(int beatID);

        [OperationContract]
        bool isLicenseAvailable(int beatID, string licenseType);

        // ============================================================
        // COLLABORATIONS
        // ============================================================
        [OperationContract]
        bool addCollaboration(int beatID, int producerID, decimal splitPercent);

        [OperationContract]
        bool updateCollaboration(int beatID, int producerID, decimal splitPercent);

        [OperationContract]
        bool removeCollaboration(int beatID, int producerID);

        [OperationContract]
        List<Collaboration> getCollaborationsByBeat(int beatID);

        [OperationContract]
        bool validateSplits(int beatID);

        // ============================================================
        // SHOPPING CART
        // ============================================================
        [OperationContract]
        bool addToCart(int userID, int beatID, string licenseType);

        [OperationContract]
        bool removeFromCart(int userID, int beatID);

        [OperationContract]
        bool clearCart(int userID);

        [OperationContract]
        List<ShoppingCart> getCart(int userID);

        [OperationContract]
        decimal getCartTotal(int userID);

        // ============================================================
        // ORDERS & TRANSACTIONS
        // ============================================================
        [OperationContract]
        int createOrder(int userID);

        [OperationContract]
        Order getOrderByID(int orderID);

        [OperationContract]
        List<Order> getOrdersByUser(int userID);

        [OperationContract]
        bool processPayment(int orderID, decimal amount, string paymentMethod);

        [OperationContract]
        bool completeOrder(int orderID);

        [OperationContract]
        bool refundOrder(int orderID);

        // ============================================================
        // AGREEMENTS
        // ============================================================
        [OperationContract]
        int signAgreement(int orderItemID, int producerID, int userID);

        [OperationContract]
        bool generateAgreementPDF(int agreementID);

        [OperationContract]
        Agreement getAgreementByID(int agreementID);

        [OperationContract]
        Agreement getAgreementByOrderItem(int orderItemID);

        [OperationContract]
        string getAgreementPDFUrl(int agreementID);

        // ============================================================
        // PAYOUTS & SPLITS
        // ============================================================
        [OperationContract]
        bool createPayout(int producerID, decimal amount);

        [OperationContract]
        bool approvePayout(int payoutID);

        [OperationContract]
        bool markPayoutPaid(int payoutID);

        [OperationContract]
        List<Payout> getPayoutsByProducer(int producerID);

        [OperationContract]
        bool createSplit(int orderItemID, int producerID, decimal percentage, decimal amount);

        [OperationContract]
        List<Split> getSplitsByOrderItem(int orderItemID);

        [OperationContract]
        List<Split> getSplitsByProducer(int producerID);

        // ============================================================
        // GENRE MANAGEMENT
        // ============================================================
        [OperationContract]
        int addGenre(string genreName);

        [OperationContract]
        List<Genre> getAllGenres();

        // ============================================================
        // PRODUCER DASHBOARD
        // ============================================================
        [OperationContract]
        int getTotalBeats(int producerID);

        [OperationContract]
        int getTotalSales(int producerID);

        [OperationContract]
        decimal getTotalRevenue(int producerID);

        [OperationContract]
        decimal getAverageSale(int producerID);

        [OperationContract]
        string getTopBeatTitle(int producerID);

        [OperationContract]
        List<OrderItem> getSalesHistory(int producerID);

        // ====================== MODEL METHODS =======================

        // ============================================================
        // REPORT METHODS
        // ============================================================

        [OperationContract]
        List<UserRegistrationStats> getRegisteredUsersPerDay(DateTime startDate, DateTime endDate);

        [OperationContract]
        decimal getTotalRevenueByDateRange(DateTime startDate, DateTime endDate);

        [OperationContract]
        List<GenreSalesStats> getSalesByGenre();

        [OperationContract]
        List<ProducerSalesStats> getTopSellingProducers(int count);

        [OperationContract]
        List<MonthlySalesStats> getMonthlySales(int year, int month);

        // ============================================================
        // INVOICE METHODS
        // ============================================================

        [OperationContract]
        Invoice getInvoiceByOrderID(int orderID);

        [OperationContract]
        List<Invoice> getAllInvoicesByUser(int userID);

        // ============================================================
        // CART METHOD
        // ============================================================

        [OperationContract]
        bool updateCartItemLicense(int userID, int beatID, string newLicenseType);

        // ============================================================
        // TRANSACTION METHODS
        // ============================================================

        [OperationContract]
        decimal calculateDiscount(int userID, decimal total);

        [OperationContract]
        int getLoyaltyPoints(int userID);
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
