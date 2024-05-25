# How-to Init the API
1. cd api
2. npm init -y
3. npm i cors express langchain pdf-parse @supabase/supabase-js

# How to setup supabase
1. https://supabase.com
2. Create new Project
3. Get URL/AnonKey
4. Run supabase.sql in SQL Editor

# How to setup Open AI
1. https://platform.openai.com/api-keys
2. Create new Secret
3. Get API Key

# Add API Keys to project
1. Add URL/AnonKey to api/lib/supabase.js
2. Add API api/lib/openai.js

# How to run
1. cd api
2. node server.js