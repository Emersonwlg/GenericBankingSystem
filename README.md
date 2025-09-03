# Banking System with Generics (C# Console App)

This project demonstrates the use of **generics** in C# through a simple **banking system**.

---

## 📌 Features

### Entities
- **Customer** → represents a bank customer with `Name` and `Document`.
- **BankAccount** → represents a bank account with `AccountNumber` and `Balance`.  
  - Supports: `Deposit`, `Withdraw`, and `TransferTo`.

### Generic Repository
- **Repository<T>** stores and manages any entity that inherits from `BaseEntity`.  
- Provides methods to **add**, **retrieve**, and **search** for items.

### Utility Class
- **Util.PrintList<T>** prints lists of any type.

---

## 🚀 Program Flow
1. Creates two customers and two accounts.  
2. Performs operations: deposit, withdraw, and transfer.  
3. Displays updated account balances.  
4. Searches for a customer by name.  

---

## 💡 Key Concepts
- **Generics** for reusability and type safety.  
- **Repository pattern** to abstract data management.  
- **Encapsulation of business logic** inside entities.  
