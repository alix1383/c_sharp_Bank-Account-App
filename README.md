# مستندات برنامه حساب بانکی (نسخه کنسولی)

## ✉ معرفی

این برنامه یک شبیه‌ساز ساده سیستم مدیریت حساب بانکی است که با زبان #C به صورت **برنامه کنسولی** نوشته شده است. این سیستم به شما امکان می‌دهد که:

- حساب جدید باز کنید
- پول واریز کنید
- پول برداشت کنید
- اطلاعات حساب را مشاهده کنید
- اطلاعات حساب را ویرایش کنید

> توجه: اطلاعات حساب‌ها در حافظه (با استفاده از `List<Account>`) ذخیره می‌شوند و هیچ پایگاه داده یا فایل خارجی استفاده نمی‌شود.

---

## 📁 ساختار برنامه

برنامه شامل دو کلاس اصلی است:

- `Program`: شامل منو، حلقه‌ی اصلی و منطق عملکرد کاربر
- `Account`: نمایش‌دهنده هر حساب بانکی با شناسه یکتا، نام، نوع و موجودی

---

## 🔢 کد به تفکیک بخش‌ها و توضیح آن‌ها

### کلاس `Account`

```csharp
class Account
{
    private static int count = 1;
    public int Id { get; private set; }
    public string Name { get; set; }
    public string Type { get; private set; }
    public decimal Balance { get; private set; }

    public Account(string name, string type)
    {
        Id = count++;
        Name = name;
        Type = type;
        Balance = 0;
    }

    public void Deposit(decimal amount) => Balance += amount;

    public bool Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Account ID: {Id}, Name: {Name}, Type: {Type}, Balance: {Balance}";
    }
}
```

### توضیح:

- هر حساب دارای شناسه‌ی یکتا (Id)، نام دارنده حساب، نوع حساب و موجودی است.
- `Deposit`: افزایش موجودی
- `Withdraw`: کاهش موجودی در صورت کافی بودن
- `ToString`: بازگرداندن اطلاعات حساب به صورت رشته برای چاپ

---

### کلاس `Program`

```csharp
class Program
{
    static List<Account> accounts = new List<Account>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1. Open Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. View Account Info");
            Console.WriteLine("5. Edit Account Info");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: OpenAccount(); break;
                case 2: Deposit(); break;
                case 3: Withdraw(); break;
                case 4: ShowAccountInfo(); break;
                case 5: EditAccount(); break;
                case 6: return;
                default: Console.WriteLine("Invalid option!"); break;
            }
        }
    }
```

### توضیح:

- یک حلقه بی‌نهایت که منوی اصلی را نشان می‌دهد و منتظر انتخاب کاربر می‌ماند.
- هر گزینه به یک متد مربوطه هدایت می‌شود.

---

### متد `OpenAccount`

```csharp
static void OpenAccount()
{
    Console.Write("Account Holder Name: ");
    string name = Console.ReadLine();

    Console.Write("Account Type (Savings/Loan): ");
    string type = Console.ReadLine();

    Account account = new Account(name, type);
    accounts.Add(account);

    Console.WriteLine($"Account {type} for {name} has been opened with ID {accounts.Count}.");
}
```

### توضیح:

- نام و نوع حساب را دریافت کرده و یک شی جدید از کلاس `Account` می‌سازد و در لیست ذخیره می‌کند.

---

### متد `Deposit`

```csharp
static void Deposit()
{
    Console.Write("Account ID: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Deposit Amount: ");
    decimal amount = decimal.Parse(Console.ReadLine());

    Account account = accounts.Find(a => a.Id == id);

    if (account != null)
    {
        account.Deposit(amount);
        Console.WriteLine($"Amount {amount} has been deposited to {account.Name}'s account.");
    }
    else
    {
        Console.WriteLine("Account not found.");
    }
}
```

### توضیح:

- شناسه حساب و مبلغ واریزی را می‌گیرد، سپس اگر حساب موجود باشد، پول به آن اضافه می‌کند.

---

### متد `Withdraw`

```csharp
static void Withdraw()
{
    Console.Write("Account ID: ");
    int id = int.Parse(Console.ReadLine());

    Console.Write("Withdrawal Amount: ");
    decimal amount = decimal.Parse(Console.ReadLine());

    Account account = accounts.Find(a => a.Id == id);

    if (account != null)
    {
        if (account.Withdraw(amount))
            Console.WriteLine($"Amount {amount} has been withdrawn from {account.Name}'s account.");
        else
            Console.WriteLine("Insufficient balance.");
    }
    else
    {
        Console.WriteLine("Account not found.");
    }
}
```

### توضیح:

- بررسی می‌کند که آیا حساب موجود است و آیا موجودی کافی برای برداشت وجود دارد یا نه.

---

### متد `ShowAccountInfo`

```csharp
static void ShowAccountInfo()
{
    Console.Write("Account ID: ");
    int id = int.Parse(Console.ReadLine());

    Account account = accounts.Find(a => a.Id == id);

    if (account != null)
    {
        Console.WriteLine(account);
    }
    else
    {
        Console.WriteLine("Account not found.");
    }
}
```

### توضیح:

- شناسه حساب را گرفته و اطلاعات کامل آن را با استفاده از `ToString` نمایش می‌دهد.

---

### متد `EditAccount`

```csharp
static void EditAccount()
{
    Console.Write("Account ID: ");
    int id = int.Parse(Console.ReadLine());

    Account account = accounts.Find(a => a.Id == id);

    if (account != null)
    {
        Console.Write("New Name: ");
        account.Name = Console.ReadLine();
        Console.WriteLine("Account information updated.");
    }
    else
    {
        Console.WriteLine("Account not found.");
    }
}
```

### توضیح:

- به کاربر اجازه می‌دهد فقط نام دارنده حساب را تغییر دهد.

---

## ⚠ نکات پایانی

- هیچ ذخیره‌سازی دائمی انجام نمی‌شود؛ با بستن برنامه تمام داده‌ها از بین می‌روند.
- هیچ بررسی نوع داده یا مدیریت استثنا در برنامه انجام نشده است.
- برای استفاده عملی، نیاز به اعتبارسنجی ورودی و ذخیره‌سازی اطلاعات داریم.

---

##
