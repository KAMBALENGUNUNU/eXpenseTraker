Personal Expenses Tracking 

Overview
This is a comprehensive expense management system designed to help users track and manage their finances effectively. The system functions as an online wallet that maintains transaction records, structures budgets, and helps users meet their saving goals.

Project Structure
- expense-tracker-app - Frontend (React.js)
- backend - Backend (Swagger/WEBAPI) 
- Database: SSMS 2022 (ExpenseTrackerfinalExamdb)

![The project presentation PPT](ppt/Expense%20Tracker.pptx)

Features

User Features
1. Welcome Page
   - Initial landing page

2. **Authentication**
   - Login page
   - Signup page

3. **Dashboard**
   - Monthly income overview
   - Monthly expense summary
   - Current balance
   - Saving goals tracking
   - Recent transactions
   - Saving transactions
   - Attendance overview (day/week/month)

4. **Transaction Management**
   - Add new transactions
   - Edit existing transactions
   - Delete transactions
   - Categorize as income or expense

5. **Budget Management**
   - Create expense categories
   - Set budget allocations
   - Category features:
     - Name
     - Amount
     - Emoji selection
   - Edit/Update categories
   - Delete categories
   - Examples: rent, groceries, school fees, shopping

6. **Reports**
   - Visual charts of transactions
   - Downloadable reports
   - Transaction history

7. **Settings**
   - Profile picture management
   - Theme switching (Dark/Light mode)
   - Personal information:
     - Address
     - City
     - Phone number
     - Email
     - Username
     - Password
   - Terms and conditions

### Admin Features
1. **Admin Dashboard**
   - User overview
   - Expense analytics

2. **User Management**
   - CRUD operations for users
   - User expense monitoring

3. **Report Generation**
   - Download comprehensive user reports
   - Transaction analytics

4. **Admin Settings**
   - Profile management
   - Password changes
   - Theme preferences

## Technical Stack

### Frontend
- React.js
- TypeScript
- Responsive design
- API integration via `api.ts`

### Backend
- Swagger/OpenAPI
- RESTful API endpoints
- Data migration support

### Database
- SQL Server Management Studio (SSMS) 2022
- Database name: ExpenseTrackerfinalExamdb
- Automated data migration

## User Flow
1. User signs up
2. User logs in
3. Sets up monthly salary (income)
4. Creates budget categories
5. Manages transactions:
   - Selects category
   - Enters amount
   - Specifies transaction type (income/expense)
6. Views dashboard for financial overview

## Setup Instructions
(To be added based on specific deployment requirements)

## API Integration
- Frontend-Backend communication through `api.ts`
- Automated data synchronization
- Real-time updates

## Data Migration
- Automated data migration support
- Database schema version control
- Seamless updates

## Security Features
- User authentication
- Secure password management
- Protected API endpoints
- Role-based access control

