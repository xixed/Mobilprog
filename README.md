# Mobilprog

A cross-platform mobile application built with .NET MAUI and C#.

## Project Overview

Mobilprog is a mobile application developed using the .NET MAUI (Multi-platform App UI) framework, allowing deployment across multiple platforms including iOS and Android. The project demonstrates a clean architecture with separation of concerns through View, ViewModel, and Model layers.

## Technology Stack

- **Framework**: .NET MAUI (Multi-platform App UI)
- **Language**: C# (100%)
- **Architecture Pattern**: MVVM (Model-View-ViewModel)
- **UI Markup**: XAML
- **Database**: SQLite (integrated via Database.cs)

## Project Structure

```
Mobilprog/
├── View/                 # UI Views (XAML pages)
├── ViewModel/           # ViewModels handling business logic and state
├── Models/              # Data models
├── Platforms/           # Platform-specific implementations (iOS, Android)
├── Resources/           # Images, fonts, and other resources
├── Properties/          # Project properties
├── App.xaml             # Application root definition
├── AppShell.xaml        # Navigation shell
├── Database.cs          # Database operations
├── MauiProgram.cs       # MAUI application configuration
├── GlobalXmlns.cs       # Global XML namespace definitions
└── Mobilprog.csproj     # Project file
```

### Key Components

- **App.xaml / App.xaml.cs**: Application root and initialization
- **AppShell.xaml / AppShell.xaml.cs**: Navigation shell for routing between pages
- **Database.cs**: Database access layer for SQLite operations
- **MauiProgram.cs**: MAUI service registration and configuration
- **View/**: Contains XAML pages and UI components
- **ViewModel/**: Contains ViewModels that manage page logic and state
- **Models/**: Data models used throughout the application

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download) or later
- Visual Studio 2022 (with MAUI workload) or Visual Studio Code
- Platform-specific requirements:
  - **iOS**: Xcode 14+ (macOS)
  - **Android**: Android SDK 21+ (API Level 21)

### Installation

1. **Clone the repository**:
   ```bash
   git clone https://github.com/xixed/Mobilprog.git
   cd Mobilprog
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the project**:
   ```bash
   dotnet build
   ```

### Running the Application

#### Debug Locally (Windows/macOS)
```bash
dotnet build -t:Run -f net8.0-windows
# or for macOS
dotnet build -t:Run -f net8.0-maccatalyst
```

#### Build for Specific Platforms

**Android**:
```bash
dotnet publish -f net8.0-android -c Release
```

**iOS**:
```bash
dotnet publish -f net8.0-ios -c Release
```

## Architecture

This project follows the **MVVM (Model-View-ViewModel)** pattern:

- **Models**: Represent the data structure
- **Views**: XAML UI definitions and code-behind
- **ViewModels**: Contain business logic and manage application state
- **Database Layer**: Handles all data persistence operations

## Features

- Cross-platform mobile development with .NET MAUI
- MVVM pattern for maintainable code structure
- SQLite database integration
- XAML-based UI design
- Multi-platform deployment (iOS, Android, Windows, macOS)

## Development

### Code Structure Best Practices

- Keep Views simple and focused on presentation
- Place business logic in ViewModels
- Use Models for data representation
- Implement INotifyPropertyChanged for data binding

### Building and Testing

```bash
# Build all platforms
dotnet build

# Run unit tests (if added)
dotnet test

# Format code
dotnet format
```

## Dependencies

See `Mobilprog.csproj` for a complete list of NuGet package dependencies.

## Repository Information

- **Created**: November 5, 2025
- **Last Updated**: Recently
- **Default Branch**: master
- **Language**: 100% C#
- **License**: Not specified

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Submit a pull request

## Resources

- [.NET MAUI Documentation](https://learn.microsoft.com/en-us/dotnet/maui/)
- [MVVM Pattern](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm)
- [XAML Documentation](https://learn.microsoft.com/en-us/dotnet/maui/xaml/)
- [GitHub Repository](https://github.com/xixed/Mobilprog)

## Support

For issues, questions, or suggestions, please open an issue on the [GitHub Issues](https://github.com/xixed/Mobilprog/issues) page.

---
