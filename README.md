# 🦁 ZooFoodCostCalculator

A clean architecture-based console application that calculates the daily cost of feeding animals in a zoo based on their dietary types, weights, and current food prices.

---

## 📐 Architecture

This project follows **Clean Architecture** principles and consists of the following layers:

- **Domain**: Contains core models and exceptions.
- **Application**: Orchestrates business logic using feeding strategies and calculation services.
- **Infrastructure**: Handles file input parsing (TXT, CSV, XML).
- **Console**: Entry point for CLI execution.

---

## 📦 Features

- Calculates food costs for carnivores, herbivores, and omnivores.
- Parses input data from structured files (`.txt`, `.csv`, `.xml`).
- Uses strategy pattern to apply diet-specific calculation logic.
- Supports custom diet ratios for omnivores (e.g., 90% meat, 10% fruit).
- Uses `ILogger<T>` for structured logging.
- Includes unit test coverage for services, strategies, and file parsers.

---

## 🚀 Getting Started

### 🛠 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- Visual Studio 2022+ or any .NET-compatible IDE

### 📁 Input Files (located under `/Files`)

- `prices.txt`: Food prices (meat, fruit)
- `animals.csv`: Species definitions and diet rates
- `zoo.xml`: Animals in the zoo with names and weights

### ▶️ Run via CLI

```bash
cd ZooFoodCostCalculator.Console
dotnet run
```

This will load the sample data from the `Files/` directory and print the total cost.

---

## 📂 File Format Details

### 🧾 `prices.txt`

```text
Meat=12.56
Fruit=5.60
```

### 🧾 `animals.csv`

```csv
Lion;0.10;meat;
Wolf;0.07;both;90%
Piranha;0.50;both;50%
```

### 🧾 `zoo.xml`

```xml
<Zoo>
  <Lions>
    <Lion name="Simba" kg="160"/>
  </Lions>
  <Piranhas>
    <Piranha name="Anastasia" kg="0.5"/>
  </Piranhas>
</Zoo>
```

---

## 🧪 Testing

Tests are located in the `Tests/` directory and cover:

- Feeding strategies
- Animal model construction
- Price and zoo file parsing
- Daily cost calculation

Run tests using:

```bash
dotnet test
```

---

## 🪵 Logging

Logging is handled using the built-in `ILogger<T>` interface. Each feeding strategy logs:

- Animal being processed
- Weight and food type breakdown
- Final calculated cost

---

## 🛡 Error Handling

- `DietNotSupportedException`: Thrown when an unsupported diet type is encountered.
- Input parsing includes basic guards but assumes well-formed structure as per assignment spec.

---

## 📈 Future Improvements

- Add more granular exceptions for missing species or malformed input
- Add integration tests and CLI-level validation
- Support output to JSON or CSV
- Improve file configuration via CLI arguments

---

## 👤 Author

**Abdullah Elmenawy**  
[GitHub Profile](https://github.com/abdullah-elmenawy)

---

## 📄 License

This project is licensed under the MIT License.
