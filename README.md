# Unit Converter

![Vue 3](https://img.shields.io/badge/Vue-3-42b883)
![.NET](https://img.shields.io/badge/.NET-8.0-512bd4)
![License](https://img.shields.io/badge/license-MIT-blue)

**Project URL:** This project is based on the [Unit Converter project from roadmap.sh](https://roadmap.sh/projects/unit-converter)


## Features

- **Multi-Mode Conversion Support**
  - Length (millimeters, centimeters, meters, kilometers, inches, feet, yards, miles)
  - Temperature (Celsius, Fahrenheit, Kelvin)
  - Weight (milligrams, grams, kilograms, ounces, pounds)

- **Modern UI/UX**
  - Material Design interface using Vuetify 3
  - Dynamic color theming per conversion mode
  - Animated tab navigation with smooth transitions
  - Calculator-style card interface
  - Responsive design

- **Single Page Application (SPA)**
  - Fast, seamless mode switching
  - No page reloads required
  - Clean, intuitive user experience

## Tech Stack

### Frontend
- **Vue 3** - Progressive JavaScript framework
- **Vuetify 3** - Material Design component framework
- **Vite** - Next-generation frontend build tool
- **Vue Router** - Official routing library for Vue.js

### Backend
- **ASP.NET Core 8.0** - Cross-platform web framework
- **C#** - Modern, type-safe programming language
- **Minimal APIs** - Lightweight API architecture

## Prerequisites

- **Node.js** (v20.19.0 or v22.12.0+)
- **.NET SDK 8.0** or higher

## Quick Start
```bash
# Terminal 1 - Start backend
cd backend && dotnet run

# Terminal 2 - Start frontend  
cd client && npm install && npm run dev
```

Then open `http://localhost:5174`

## Getting Started

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the server:
   ```bash
   dotnet run
   ```

   The API will start on `http://localhost:5289`

### Frontend Setup

1. Navigate to the client directory:
   ```bash
   cd client
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Start the development server:
   ```bash
   npm run dev
   ```

   The application will be available at `http://localhost:5174` (or `http://localhost:5173` if that port is free)

## API Endpoints

### Conversion Endpoints

- **POST** `/convert/length` - Convert length measurements
- **POST** `/convert/temperature` - Convert temperature measurements
- **POST** `/convert/weight` - Convert weight measurements

**Request Body:**
```json
{
  "Value": 100,
  "FromUnit": "meters",
  "ToUnit": "feet"
}
```

**Response:**
```json
{
  "result": 328.084,
  "originalValue": 100,
  "fromUnit": "meters",
  "toUnit": "feet",
  "fromUnitSymbol": "m",
  "toUnitSymbol": "ft"
}
```

### Units Endpoints

- **GET** `/convert/length/units` - Get all available length units
- **GET** `/convert/temperature/units` - Get all available temperature units
- **GET** `/convert/weight/units` - Get all available weight units

**Response:**
```json
["meters", "feet", "inches", ...]
```

## Project Structure

```
unit-converter/
├── backend/                 # ASP.NET Core backend
│   ├── Program.cs          # API routes and configuration
│   ├── Conversions.cs      # Conversion logic for all units
│   ├── Models.cs           # Request/Response models
│   └── unit-converter.csproj
│
├── client/                  # Vue.js frontend
│   ├── src/
│   │   ├── components/
│   │   │   └── ConversionForm.vue  # Main conversion component
│   │   ├── plugins/
│   │   │   └── vuetify.js          # Vuetify configuration
│   │   ├── App.vue                 # Root component with tab navigation
│   │   ├── main.js                 # Application entry point
│   │   └── config.js               # API configuration
│   ├── package.json
│   └── vite.config.js
│
└── README.md
```

## Features in Detail

### Dynamic Theming
Each conversion mode has its own color scheme:
- **Length**: Blue (#1867C0)
- **Temperature**: Red (#D32F2F)
- **Weight**: Green (#388E3C)

The entire interface updates to match the selected mode, providing clear visual feedback.

### Conversion Logic
All conversions follow a standard-based approach:
- **Length**: Converts through meters as the base unit
- **Temperature**: Direct mathematical formulas for each conversion
- **Weight**: Converts through grams as the base unit

Results are rounded to 4 decimal places for precision.

## Development

### Frontend Development
```bash
cd client
npm run dev        # Start development server
npm run build      # Build for production
npm run preview    # Preview production build
npm run lint       # Lint and fix code
npm run format     # Format code with Prettier
```

### Backend Development
```bash
cd backend
dotnet build       # Build the project
dotnet run         # Run the server
dotnet watch       # Run with hot reload
```

## Troubleshooting

**Backend won't start:**
- Ensure .NET 8.0 SDK is installed: `dotnet --version`
- Check if port 5289 is already in use

**Frontend CORS errors:**
- Verify backend is running on `http://localhost:5289`
- Check CORS configuration in `backend/Program.cs`

**Conversion returns unexpected results:**
- Verify unit names match exactly (case-insensitive but must be spelled correctly)
- Check that both "From" and "To" units are selected

## Configuration

### Frontend API Configuration
Update the API base URL in `client/src/config.js`:
```javascript
export const API_BASE_URL = 'http://localhost:5289'
```

### Backend Port Configuration
The backend port is configured in `backend/Properties/launchSettings.json`

### CORS Configuration

The backend is configured to accept requests from `http://localhost:5173` and `http://localhost:5174`. If your frontend runs on a different port, update the CORS policy in `backend/Program.cs`:
```csharp
policy.WithOrigins("http://localhost:YOUR_PORT")
```

## Browser Support

- Chrome/Edge (latest)
- Firefox (latest)
- Safari (latest)

Good call being transparent about that. Here's how I'd add it:

---

## Development Notes
### What I Learned

This project helped me practice:
- Building RESTful APIs with ASP.NET Core Minimal APIs
- Implementing CORS for cross-origin requests
- Creating reusable Vue 3 components with props
- Working with Vuetify's Material Design components
- Structuring a full-stack application (frontend + backend)
- Applying the Canonical Form pattern to reduce conversion complexity

### AI Tool Usage

Claude Code was used for specific frontend tasks to accelerate development:
- **Vuetify Integration**: Initial setup and configuration of Vuetify 3 components
- **UI Component Migration**: Converting plain HTML elements to Vuetify components (buttons, cards, navigation)
- **Code Duplication**: Generalizing the length conversion form to weight and temperature modes

**All core logic was hand-written:**
- Backend API design and implementation (ASP.NET Core)
- Conversion algorithms and canonical form pattern
- Request/response models and validation
- Frontend conversion logic and state management
- API integration and CORS configuration

This approach allowed me to focus on architectural decisions and business logic while leveraging tooling for repetitive UI tasks.

## License

This project is open source and available for educational purposes.

## Acknowledgments

- Project based on [roadmap.sh Unit Converter project](https://roadmap.sh/projects/unit-converter)
- Built with Vue 3 and Vuetify 3
- Backend powered by ASP.NET Core 8.0
