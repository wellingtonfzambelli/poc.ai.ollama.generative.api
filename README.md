# About
This project is a Proof of Concept (P.O.C.) designed to demonstrate how to integrate Ollama with a .NET 9 Web API using SQLite as the database. The main goal of this P.O.C. is to showcase practical patterns for consuming large language models (LLMs) in backend applications, focusing on clarity, control, and real-world applicability rather than chatbot-style interactions.  

The first endpoint illustrates a basic integration with Ollama, where the API sends a user prompt directly to the LLM and returns a natural language response. This approach is useful for scenarios such as question answering, content generation, or general assistant-like behavior, and serves as an entry point to understand how Ollama can be consumed from a .NET application using a clean and minimal setup.  

The second endpoint demonstrates a more system-oriented use case, commonly found in enterprise applications. In this flow, the API interacts with Ollama twice: first, to translate a natural language question into a SQL query based on the database schema, and second, to convert the raw query results (returned as JSON) into a concise and human-readable English response. This pattern highlights how LLMs can be safely orchestrated to work alongside relational databases, enabling natural language access to structured data while keeping business logic and execution fully under backend control.

# Stacks of this project
- .NET 9
- Ollama
- Entity Framework Core
- SQL Lite
- Docker
- Visual Studio Community

# Flow
Integrating with Ollama
<img width="1536" height="1024" alt="ollama-integration" src="https://github.com/user-attachments/assets/49e46966-32a1-4179-a2c5-2d66dd2d88d8" />

Extracting from database using Ollama with natural language  
<img width="1172" height="1216" alt="ollama-flow" src="https://github.com/user-attachments/assets/faeddd45-bcbb-4ed8-be47-5c698e744509" />


# Swagger
<img width="1469" height="643" alt="image" src="https://github.com/user-attachments/assets/2ccd3f34-ac41-4c95-91a9-fce3b646d390" />
<img width="1346" height="956" alt="image" src="https://github.com/user-attachments/assets/ce760601-e3ee-46c0-8486-bafb2b6c29e8" />
<img width="1301" height="900" alt="image" src="https://github.com/user-attachments/assets/0da9d3ba-c785-416a-9691-af01fb085f9e" />


# Docker / Containers
<img width="772" height="367" alt="image" src="https://github.com/user-attachments/assets/9f9438ef-5350-4ab6-9c84-c02cb6e9d88c" />
<img width="528" height="527" alt="image" src="https://github.com/user-attachments/assets/7ee54006-2851-4ff7-b9ba-db440c11d24d" />
<img width="1596" height="255" alt="image" src="https://github.com/user-attachments/assets/e1eb7e69-62b2-4c75-99cf-cbb9c20d33e3" />

# Sql Lite
<img width="730" height="730" alt="image" src="https://github.com/user-attachments/assets/80b458e5-748a-4a86-97ee-d323627a2e3d" />

