# Railway Profit Tuner (Cities: Skylines 1 Mod)

[![Platform](https://img.shields.io/badge/Platform-PC-blue.svg)](https://www.paradoxinteractive.com/games/cities-skylines)
[![Mod Version](https://img.shields.io/badge/Mod_Version-v1.0.0-green.svg)]()
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A lightweight **Cities: Skylines** mod that reconstructs the cargo train income logic based on distance and cargo size. Built with **Harmony Lib**.

## 🚀 Overview

By default, cargo rail maintenance in Cities: Skylines can be a financial burden. This mod adds a realistic revenue stream by calculating a "delivery fee" whenever a cargo train arrives at its destination.

### Formula

$$TotalIncome = (BaseFee + (Distance \times 50) + (Cargo \times 20)) \times Multiplier$$

## 🛠 Features

- **Dynamic Calculation**: Real-time distance and load-based rewards.
- **In-game Settings**: Adjust base fee and multiplier via the Options menu.
- **Persistent Config**: Settings are saved to `RailwayProfitConfig.xml` in the game's local folder.
- **Performance Optimized**: Uses Harmony Postfix to ensure zero impact on core simulation logic.

## 💻 Development

### Prerequisites

- .NET Framework 3.5 (Required by CSL 1)
- Harmony (Cities Harmony API)
- Cities: Skylines Binaries (`ICities.dll`, `ColossalFramework.dll`, etc.)

### Build Instructions

1. Clone the repository.
2. Reference the required game DLLs from your Steam installation folder.
3. Build the project as a **Class Library (.dll)**.
4. Copy the output DLL to `%LocalAppData%\Colossal Order\Cities_Skylines\Addons\Mods\RailwayProfitMod\`.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
