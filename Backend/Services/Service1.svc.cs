using EveryBeats.Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using frontend;
using System.Web;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using EveryBeats.Backend.Models;

namespace EveryBeats
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        EveryBeatsDataContextDataContext db = new EveryBeatsDataContextDataContext();
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

        // ============================================================
        // USER MANAGEMENT
        // ============================================================

        public bool loginUser(string email, string password)
        {
            var user = (from u in db.Users where u.email == email && u.password_hash == SecrecyHash.hashFunction(password) select u).SingleOrDefault();
            return user != null;
        }

        public int registerUser(User newUser)
        {
            // Check if email already exists
            var existing = (from u in db.Users where u.email == newUser.email select u).SingleOrDefault();
            if (existing != null) return 2;

            try
            {
                db.Users.InsertOnSubmit(newUser);
                db.SubmitChanges();
                return 0; // User registered successfully
            }
            catch(Exception)
            {
                return 1; // Database connection issues
            }
        }

        public bool updateUserProfile(int userID, string firstName, string lastName, string cellnumber)
        {
            var user = (from u in db.Users where u.userID == userID select u).SingleOrDefault();
            if (user == null) return false;

            user.first_name = firstName;
            user.last_name = lastName;
            user.cellnumber = cellnumber;
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool deactivateUser(int userID)
        {
            var user = (from u in db.Users where u.userID == userID select u).SingleOrDefault();
            if (user == null) return false;

            user.is_active = false;
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public User getUserByID(int userID)
        {
            return (from u in db.Users where u.userID == userID select u).SingleOrDefault();
        }

        public List<User> getAllUsers()
        {
            return (from u in db.Users where u.is_active == true select u).ToList();
        }

        // ============================================================
        // BEAT MANAGEMENT
        // ============================================================

        public int uploadBeat(Beat newBeat)
        {
            try
            {
                db.Beats.InsertOnSubmit(newBeat);
                db.SubmitChanges();
                return newBeat.beat_id;
            }
            catch
            {
                return 0;
            }
        }

        public bool updateBeat(Beat beat)
        {
            var existing = (from b in db.Beats where b.beat_id == beat.beat_id select b).SingleOrDefault();
            if (existing == null) return false;

            existing.title = beat.title;
            existing.genre_id = beat.genre_id;
            existing.bmp = beat.bmp;
            existing.music_key = beat.music_key;
            existing.mp3_file = beat.mp3_file;
            existing.wav_file = beat.wav_file;
            existing.stems_file = beat.stems_file;
            existing.is_active = beat.is_active;
            
            try {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool deleteBeat(int beatID)
        {
            var beat = (from b in db.Beats where b.beat_id == beatID select b).SingleOrDefault();
            if (beat == null) return false;

            beat.is_active = false;
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public Beat getBeatByID(int beatID)
        {
            return (from b in db.Beats where b.beat_id == beatID && b.is_active == true select b).SingleOrDefault();
        }

        public List<Beat> getAllBeats()
        {
            return (from b in db.Beats where b.is_active == true select b).ToList();
        }

        public List<Beat> getBeatsByProducer(int producerID)
        {
            return (from b in db.Beats where b.producer_id == producerID && b.is_active == true select b).ToList();
        }

        public List<Beat> getBeatsByGenre(int genreID)
        {
            return (from b in db.Beats where b.genre_id == genreID && b.is_active == true select b).ToList();
        }

        public List<Beat> searchBeats(string keyword)
        {
            return (from b in db.Beats where b.title.Contains(keyword) && b.is_active == true select b).ToList();
        }

        // ============================================================
        // LICENSE MANAGEMENT
        // ============================================================

        public int addLicense(License newLicense)
        {
            try
            {
                db.Licenses.InsertOnSubmit(newLicense);
                db.SubmitChanges();
                return newLicense.license_id;
            }
            catch
            {
                return 0;
            }
        }

        public bool updateLicense(License license)
        {
            var existing = (from l in db.Licenses where l.license_id == license.license_id select l).SingleOrDefault();
            if (existing == null) return false;

            existing.license_type = license.license_type;
            existing.terms = license.terms;
            existing.price = license.price;
            existing.is_active = license.is_active;
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool deleteLicense(int licenseID)
        {
            var license = (from l in db.Licenses where l.license_id == licenseID select l).SingleOrDefault();
            if (license == null) return false;

            license.is_active = false;
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public License getLicenseByID(int licenseID)
        {
          return (from l in db.Licenses where l.license_id == licenseID && l.is_active ==true select l).SingleOrDefault();
        }

        public List<License> getLicensesByBeat(int beatID)
        {
            return (from l in db.Licenses where l.beat_id == beatID && l.is_active == true select l).ToList();
        }

        public bool isLicenseAvailable(int beatID, string licenseType)
        {
            if (licenseType.ToLower().Equals("exclusive"))
            {
                // Check if any order item has this exclusive license
                var sold = (from l in db.Licenses
                            join oi in db.OrderItems on l.license_id equals oi.license_id
                            where l.beat_id == beatID && l.license_type.ToLower().Equals("exclusive")
                            select oi).Any();
                return !sold;
            }
            return true; // Lease is always available
        }

        // ============================================================
        // COLLABORATIONS
        // ============================================================

        public bool addCollaboration(int beatID, int producerID, decimal splitPercent)
        {
            try
            {
                var collab = new Collaboration
                {
                    beat_id = beatID,
                    producer_id = producerID,
                    split_percent = splitPercent
                };
                db.Collaborations.InsertOnSubmit(collab);
                db.SubmitChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool updateCollaboration(int beatID, int producerID, decimal splitPercent)
        {
            var collab = (from c in db.Collaborations where c.beat_id == beatID && c.producer_id == producerID select c).SingleOrDefault();
            if (collab == null) return false;

            collab.split_percent = splitPercent;
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }

        }

        public bool removeCollaboration(int beatID, int producerID)
        {
            var collab = (from c in db.Collaborations where c.beat_id == beatID && c.producer_id == producerID select c).SingleOrDefault();
            if (collab == null) return false;
            try
            {
                db.Collaborations.DeleteOnSubmit(collab);
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public List<Collaboration> getCollaborationsByBeat(int beatID)
        {
            return (from c in db.Collaborations where c.beat_id == beatID select c).ToList();
        }

        public bool validateSplits(int beatID)
        {
            var splits = getCollaborationsByBeat(beatID);
            decimal total = 0.0m;
            foreach (var split in splits) 
            {
                total += split.split_percent;
            }
            return total == 100.00m;
        }

        // ============================================================
        // SHOPPING CART
        // ============================================================

        public bool addToCart(int userID, int beatID, string licenseType)
        {
            // Check if already in cart
           var existing = (from s in db.ShoppingCarts where s.userID == userID && s.beat_id == beatID select s).SingleOrDefault();
            if (existing != null) return false;

            // Check license availability
            if (!isLicenseAvailable(beatID, licenseType)) return false;

            var cartItem = new ShoppingCart
            {
                userID = userID,
                beat_id = beatID,
                license_type = licenseType,
                date_added = DateTime.Now
            };

            try
            {
                db.ShoppingCarts.InsertOnSubmit(cartItem);
                db.SubmitChanges();
                return true;
            }catch(Exception) { return false; }
        }

        public bool removeFromCart(int userID, int beatID)
        {
            var item = (from s in db.ShoppingCarts where s.userID == userID && s.beat_id == beatID select s).SingleOrDefault();
            if (item == null) return false;

            try
            {
                db.ShoppingCarts.DeleteOnSubmit(item);
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public bool clearCart(int userID)
        {
            var items = (from s in db.ShoppingCarts where s.userID == userID select s);
            if (!items.Any()) return false;

            try
            {
                db.ShoppingCarts.DeleteAllOnSubmit(items);
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public List<ShoppingCart> getCart(int userID)
        {
            return (from s in db.ShoppingCarts where s.userID == userID select s).ToList();
        }

        public decimal getCartTotal(int userID)
        {
            var cartItems = from c in db.ShoppingCarts
                            from b in db.Beats
                            from l in db.Licenses
                            where c.beat_id == b.beat_id
                               && b.beat_id == l.beat_id
                               && c.license_type == l.license_type
                               && c.userID == userID
                               && l.is_active == true
                            select l.price;

            return cartItems.Sum();
        }

        // ============================================================
        // ORDERS & TRANSACTIONS
        // ============================================================

        public int createOrder(int userID)
        {
            try
            {
                // Get cart total
                decimal total = getCartTotal(userID);

                // Calculate loyalty discount
                decimal discount = calculateLoyaltyDiscount(userID, 0);

                // Apply discount
                decimal finalTotal = total - discount;

                var order = new Order
                {
                    userID = userID,
                    total_amount = finalTotal,
                    OrderDate = DateTime.Now,
                    status = "pending"
                };

                db.Orders.InsertOnSubmit(order);
                db.SubmitChanges();

                // Move cart items to order items
                var cartItems = (from s in db.ShoppingCarts where s.userID == userID select s).ToList();

                foreach (var item in cartItems)
                {
                    var license = (from l in db.Licenses where l.beat_id == item.beat_id && l.license_type == item.license_type select l).SingleOrDefault();
                    if (license != null)
                    {
                        var orderItem = new OrderItem
                        {
                            order_id = order.order_id,
                            beat_id = item.beat_id,
                            license_id = license.license_id,
                            price_paid = license.price,
                            Order_item_date = DateTime.Now
                        };
                        db.OrderItems.InsertOnSubmit(orderItem);
                    }
                }

                // Clear cart
                db.ShoppingCarts.DeleteAllOnSubmit(cartItems);
                db.SubmitChanges();

                return order.order_id;
            }
            catch
            {
                return 0;
            }
        }

        public Order getOrderByID(int orderID)
        {
            return (from o in db.Orders where o.order_id == orderID select o).SingleOrDefault();
        }

        public List<Order> getOrdersByUser(int userID)
        {
            return (from o in db.Orders where o.userID == userID select o).ToList();
        }

        public bool processPayment(int orderID, decimal amount, string paymentMethod)
        {
            var order = getOrderByID(orderID);
            if (order == null || order.status != "pending") return false;

            // Check if all order items have signed agreements
            var orderItems = (from oi in db.OrderItems where oi.order_id == orderID select oi).ToList();

            // Validate Payment Method
            List<string> allowedMethods = new List<string> { "mock", "eft", "payfast", "yoco", "snapscan" };
            if (!allowedMethods.Contains(paymentMethod.ToLower()))
            {
                return false;
            }

            foreach (var item in orderItems)
            {
                var agreement = getAgreementByOrderItem(item.order_item_id);
                if (agreement == null || !agreement.IsSigned)
                {
                    return false; // if Not signed we the block payment
                }
            }

            //Handle the different payment methods
            switch (paymentMethod.ToLower())
            {
                case "mock":
                    // Mock payment — always succeeds
                    break;

                case "eft":
                    // Bank transfer 
                    break;

                case "payfast":
                    // In real implementation, we would call PayFast API here
                    // It simulates success for now
                    break;

                case "yoco":
                    // In real implementation, we would call Yoco API here
                    // It simulates success for now
                    break;

                case "snapscan":
                    // In real implementation, we would call SnapScan API here
                    // It simulates success for now
                    break;

                default:
                    return false; // Unknown payment method
            }

            // Process payment (mock)
            order.status = "paid";
            db.SubmitChanges();

            // Generate PDF for each signed agreement after payment
            foreach (var item in orderItems)
            {
                var agreement = getAgreementByOrderItem(item.order_item_id);
                if (agreement != null && string.IsNullOrEmpty(agreement.signed_pdf_url))
                {
                   generateAgreementPDF(agreement.agreement_id);                
                }
            }

            return true;
        }

        public bool completeOrder(int orderID)
        {
            var order = getOrderByID(orderID);
            if (order == null || order.status != "paid") return false;

            // Ensure all agreements have PDFs
            var orderItems = (from oi in db.OrderItems where oi.order_id == orderID select oi).ToList();

            foreach (var item in orderItems)
            {
                var agreement = getAgreementByOrderItem(item.order_item_id);
                if (agreement != null && string.IsNullOrEmpty(agreement.signed_pdf_url))
                {
                    generateAgreementPDF(agreement.agreement_id);
                }
            }

            order.status = "completed";
            db.SubmitChanges();
            return true;
        }

        public bool refundOrder(int orderID)
        {
            var order = getOrderByID(orderID);
            if (order == null || order.status == "refunded") return false;

            // Invalidate agreement
            var orderItems = (from oi in db.OrderItems where oi.order_id == orderID select oi).ToList();

            foreach (var item in orderItems)
            {
                var agreement = getAgreementByOrderItem(item.order_item_id);
                if (agreement != null)
                {
                    agreement.IsSigned = false;
                    agreement.signed_pdf_url = null; // Remove PDF link
                }
            }

            order.status = "refunded";
            db.SubmitChanges();
            return true;
        }

        // ============================================================
        // AGREEMENTS
        // ============================================================

        string BuildAgreementText(License license, Beat beat, Producer producer, User buyer, OrderItem orderItem)
        {
            string terms = license?.terms ?? "No specific terms provided.";

            return $@"
        BEAT LICENSE AGREEMENT

        Date: {DateTime.Now:yyyy-MM-dd}

        1. Parties
        Licensor (Producer): {producer?.artist_name ?? "Unknown"}
        Licensee (Buyer): {buyer?.first_name ?? ""} {buyer?.last_name ?? ""}

        2. Beat Information
        Title: {beat?.title ?? "Unknown"}
        License Type: {license?.license_type ?? "Unknown"}
        Price: R{orderItem?.price_paid ?? 0}

        3. Terms and Conditions
        {terms}

        4. Ownership
        The Licensor retains full ownership of the beat.
        The Licensee is granted usage rights as described above.

        5. Credit
        Credit must be given as: Prod. by {producer?.artist_name ?? "Unknown"}

        6. Restrictions
        - This license is non-transferable.
        - The Licensee may not register this beat with Content ID.

        Signed by:
        Producer: ____________________   Date: _________
        Buyer: ____________________      Date: _________
    ";
        }
        public int signAgreement(int orderItemID, int producerID, int userID)
        {
            try
            {
                // Check if already signed
                var existing = (from a in db.Agreements where a.order_item_id == orderItemID select a).SingleOrDefault();
                if (existing != null) return existing.agreement_id;

                // Get data for the agreement
                var orderItem = (from oi in db.OrderItems where oi.order_item_id == orderItemID select oi).SingleOrDefault();
                if (orderItem == null) return 0;

                var license = getLicenseByID(orderItem.license_id);
                var beat = getBeatByID(orderItem.beat_id);
                var producer = (from p in db.Producers where p.producer_id == producerID select p).SingleOrDefault();
                var buyer = getUserByID(userID);

                // Build agreement text using license terms
                string agreementText = BuildAgreementText(license, beat, producer, buyer, orderItem);

                // Create agreement record 
                var agreement = new Agreement
                {
                    order_item_id = orderItemID,
                    producer_id = producerID,
                    userID = userID,
                    signed_pdf_url = null, // PDF not yet generated
                    signed_at = DateTime.Now,
                    agreement_version = 1,
                    IsSigned = true
                };

                db.Agreements.InsertOnSubmit(agreement);
                db.SubmitChanges();

                return agreement.agreement_id;
            }
            catch
            {
                return 0;
            }
        }
        bool IService1.generateAgreementPDF(int agreementID) {
           return generateAgreementPDF(agreementID);
        }

        bool generateAgreementPDF(int agreementID)
        {
            try
            {
                var agreement = getAgreementByID(agreementID);
                if (agreement == null || !agreement.IsSigned) return false;

                // Check if agreementPDF exists
                if (!string.IsNullOrEmpty(agreement.signed_pdf_url)) return true;

                var orderItem = (from oi in db.OrderItems where oi.order_item_id == agreement.order_item_id select oi).SingleOrDefault();

                var license = getLicenseByID(orderItem.license_id);
                var beat = getBeatByID(orderItem.beat_id);
                var producer = (from p in db.Producers where p.producer_id == agreement.producer_id select p).SingleOrDefault();
                var buyer = getUserByID(agreement.userID);

                // Build contract
                string contractText = BuildAgreementText(license, beat, producer, buyer, orderItem);

                // Generate PDF
                string pdfFolder = HttpContext.Current.Server.MapPath("~/agreements/");
                if (!Directory.Exists(pdfFolder))
                    Directory.CreateDirectory(pdfFolder);

                string pdfPath = Path.Combine(pdfFolder, $"agreement_{agreement.order_item_id}.pdf");
                string pdfUrl = $"/agreements/agreement_{agreement.order_item_id}.pdf";

                using (FileStream fs = new FileStream(pdfPath, FileMode.Create))
                {
                    Document document = new Document();
                    PdfWriter.GetInstance(document, fs);
                    document.Open();
                    document.Add(new Paragraph(contractText));
                    document.Close();
                }

                // Update agreement with PDF URL
                agreement.signed_pdf_url = pdfUrl;
                db.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Agreement getAgreementByID(int agreementID)
        {
            return (from a in db.Agreements where a.agreement_id == agreementID select a).SingleOrDefault();
        }

        public Agreement getAgreementByOrderItem(int orderItemID)
        {
            return (from a in db.Agreements where a.order_item_id == orderItemID && a.IsSigned == true select a).SingleOrDefault();
        }

        public string getAgreementPDFUrl(int agreementID)
        {
            var agreement = getAgreementByID(agreementID);
            if (agreement == null || !agreement.IsSigned || string.IsNullOrEmpty(agreement.signed_pdf_url))
                return null;

            return agreement.signed_pdf_url;
        }

        // ============================================================
        // PAYOUTS & SPLITS
        // ============================================================

        public bool createPayout(int producerID, decimal amount)
        {
            try
            {
                var payout = new Payout
                {
                    producer_id = producerID,
                    amount = amount,
                    status = "pending"
                };
                db.Payouts.InsertOnSubmit(payout);
                db.SubmitChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool approvePayout(int payoutID)
        {
            var payout = (from p in db.Payouts where p.payout_id == payoutID select p).SingleOrDefault();
            if (payout == null) return false;

            payout.status = "approved";
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch(Exception) { return false; }
        }

        public bool markPayoutPaid(int payoutID)
        {
            var payout = (from p in db.Payouts where p.payout_id == payoutID select p).SingleOrDefault();
            if (payout == null) return false;

            payout.status = "paid";
            payout.paid_at = DateTime.Now;
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        public List<Payout> getPayoutsByProducer(int producerID)
        {
            return (from p in db.Payouts where p.producer_id == producerID select p).ToList();
        }

        public bool createSplit(int orderItemID, int producerID, decimal percentage, decimal amount)
        {
            try
            {
                var split = new Split
                {
                    order_item_id = orderItemID,
                    producer_id = producerID,
                    percentage = percentage,
                    amount = amount
                };
                db.Splits.InsertOnSubmit(split);
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Split> getSplitsByOrderItem(int orderItemID)
        {
            return (from s in db.Splits where s.order_item_id == orderItemID select s).ToList();
        }

        public List<Split> getSplitsByProducer(int producerID)
        {
            return (from s in db.Splits where s.producer_id == producerID select s).ToList();
        }

        // ============================================================
        // GENRE MANAGEMENT
        // ============================================================

        public int addGenre(string genreName)
        {
            try
            {
                var genre = new Genre { name = genreName };
                db.Genres.InsertOnSubmit(genre);
                db.SubmitChanges();
                return genre.genre_id;
            }
            catch
            {
                return 0;
            }
        }

        public List<Genre> getAllGenres()
        {
            return db.Genres.ToList();
        }

        // ============================================================
        // PRODUCER DASHBOARD
        // ============================================================

        public int getTotalBeats(int producerID)
        {
            return getBeatsByProducer(producerID).Count;
        }

        public int getTotalSales(int producerID)
        {
            return (from oi in db.OrderItems
                    join b in db.Beats on oi.beat_id equals b.beat_id
                    where b.producer_id == producerID
                    select oi).Count();
        }

        public decimal getTotalRevenue(int producerID)
        {
            return (from oi in db.OrderItems
                    join b in db.Beats on oi.beat_id equals b.beat_id
                    where b.producer_id == producerID
                    select oi.price_paid).Sum();
        }

        public decimal getAverageSale(int producerID)
        {
            var sales = from oi in db.OrderItems
                        join b in db.Beats on oi.beat_id equals b.beat_id
                        where b.producer_id == producerID
                        select oi.price_paid;

            return sales.Any() ? sales.Average() : 0;
        }

        public string getTopBeatTitle(int producerID)
        {
            var topBeat = (from oi in db.OrderItems
                           join b in db.Beats on oi.beat_id equals b.beat_id
                           where b.producer_id == producerID
                           group oi by b.title into g
                           select new { Title = g.Key, Count = g.Count() })
                           .OrderByDescending(x => x.Count)
                           .FirstOrDefault();

            return topBeat?.Title ?? "N/A";
        }

        public List<OrderItem> getSalesHistory(int producerID)
        {
            return (from oi in db.OrderItems
                    join b in db.Beats on oi.beat_id equals b.beat_id
                    where b.producer_id == producerID
                    select oi).ToList();
        }

        // ====================== MODEL METHODS =======================

        // ============================================================
        // REPORT METHODS
        // ============================================================

        List<UserRegistrationStats> IService1.getRegisteredUsersPerDay(DateTime startDate, DateTime endDate)
        {
            return (from u in db.Users
                    where u.created_at >= startDate && u.created_at <= endDate
                    group u by u.created_at into g
                    select new UserRegistrationStats
                    {
                        RegistrationDate = (DateTime)g.Key,
                        UserCount = g.Count()
                    } into stats
                    orderby stats.RegistrationDate
                    select stats)
                    .ToList();
        }

        decimal IService1.getTotalRevenueByDateRange(DateTime startDate, DateTime endDate)
        {
            return (from o in db.Orders
                    where o.OrderDate >= startDate && o.OrderDate <= endDate
                    where o.status == "completed"
                    select o.total_amount).Sum();
        }

        List<GenreSalesStats> IService1.getSalesByGenre()
        {
            return (from oi in db.OrderItems
                    join b in db.Beats on oi.beat_id equals b.beat_id
                    join g in db.Genres on b.genre_id equals g.genre_id
                    group oi by g.name into grp
                    select new GenreSalesStats
                    {
                        GenreName = grp.Key,
                        TotalSales = grp.Count(),
                        TotalRevenue = grp.Sum(x => x.price_paid)
                    } into stats
                    orderby stats.TotalSales descending
                    select stats)
                    .ToList();
        }

        List<ProducerSalesStats> IService1.getTopSellingProducers(int count)
        {
            return (from oi in db.OrderItems
                    join b in db.Beats on oi.beat_id equals b.beat_id
                    join p in db.Producers on b.producer_id equals p.producer_id
                    join u in db.Users on p.userID equals u.userID
                    group oi by new { p.producer_id, u.first_name, u.last_name } into grp
                    select new ProducerSalesStats
                    {
                        ProducerID = grp.Key.producer_id,
                        ArtistName = grp.Key.first_name + " " + grp.Key.last_name,
                        TotalSales = grp.Count(),
                        TotalRevenue = grp.Sum(x => x.price_paid)
                    } into stats
                    orderby stats.TotalSales descending
                    select stats)
        .Take(count)
        .ToList();
        }

        List<MonthlySalesStats> IService1.getMonthlySales(int year, int month)
        {
            return (from o in db.Orders
                    where o.OrderDate.Value.Year == year
                    where o.status == "completed"
                    group o by o.OrderDate.Value.Month into grp
                    select new MonthlySalesStats
                    {
                        Year = year,
                        Month = grp.Key,
                        MonthName = System.Globalization.CultureInfo.CurrentCulture
                                    .DateTimeFormat.GetMonthName(grp.Key),
                        TotalOrders = grp.Count(),
                        TotalRevenue = grp.Sum(x => x.total_amount)
                    } into stats
                    orderby stats.Month
                    select stats)
                    .ToList();
        }

        // ============================================================
        // INVOICE METHODS
        // ============================================================

        Invoice IService1.getInvoiceByOrderID(int orderID)
        {
            return getInvoiceByOrderID(orderID);           
        }

        Invoice getInvoiceByOrderID(int orderID)
        {
            var order = getOrderByID(orderID);
            if (order == null) return null;

            var user = getUserByID(order.userID);

            var items = (from oi in db.OrderItems
                         join b in db.Beats on oi.beat_id equals b.beat_id
                         join l in db.Licenses on oi.license_id equals l.license_id
                         where oi.order_id == orderID
                         select new InvoiceItem
                         {
                             BeatTitle = b.title,
                             LicenseType = l.license_type,
                             PricePaid = oi.price_paid
                         }).ToList();

            return new Invoice
            {
                OrderID = order.order_id,
                UserID = order.userID,
                UserName = (user?.first_name + " " + user?.last_name) ?? "Unknown",
                OrderDate = order.OrderDate.Value,
                TotalAmount = order.total_amount,
                Status = order.status,
                Items = items
            };
        }

        List<Invoice> IService1.getAllInvoicesByUser(int userID)
        {
            var orders = getOrdersByUser(userID);
            var invoices = new List<Invoice>();

            foreach (var order in orders)
            {
                var invoice = getInvoiceByOrderID(order.order_id);
                if (invoice != null)
                    invoices.Add(invoice);
            }
            return invoices.OrderByDescending(x => x.OrderDate).ToList();
        }

        // ============================================================
        // CART METHOD
        // ============================================================

        bool IService1.updateCartItemLicense(int userID, int beatID, string newLicenseType)
        {
            var cartItem = (from s in db.ShoppingCarts where s.userID == userID && s.beat_id == beatID select s).SingleOrDefault();

            if (cartItem == null) return false;

            // Check if the new license is available
            if (!isLicenseAvailable(beatID, newLicenseType)) return false;

            cartItem.license_type = newLicenseType;
            cartItem.date_added = DateTime.Now; // Update timestamp
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception) { return false; }
        }

        // ============================================================
        // TRANSACTION METHODS
        // ============================================================

        decimal IService1.calculateDiscount(int userID, decimal total)
        {
            decimal discount = 0;

            // Rule 1: 10% discount for users who have placed more than 5 orders
            int orderCount = (from o in db.Orders where o.userID == userID && o.status == "completed" select o).Count();

            if (orderCount >= 5)
                discount += total * 0.10m;

            // Rule 2: 5% discount for orders over R500
            if (total > 500)
                discount += total * 0.05m;

            // Rule 3: 10% discount for producers (loyalty)
            var user = getUserByID(userID);
            if (user != null && user.role == "producer")
                discount += total * 0.10m;

            return Math.Min(discount, total * 0.25m); // Max 25% discount
        }

        int IService1.getLoyaltyPoints(int userID)
        {
            return getLoyaltyPoints(userID);
        }

        int getLoyaltyPoints(int userID)
        {
            // 1 point per R10 spent on completed orders
            var totalSpent = (from o in db.Orders
                              where o.userID == userID && o.status == "completed"
                              select o.total_amount).Sum();

            return (int)(totalSpent / 10);
        }

        public decimal calculateLoyaltyDiscount(int userID, int pointsToUse)
        {
            // Get available points
            int availablePoints = getLoyaltyPoints(userID);
            if (pointsToUse > availablePoints) return 0;

            // 100 points = R10 discount
            return (pointsToUse / 100) * 10m;
        }
    }
}
