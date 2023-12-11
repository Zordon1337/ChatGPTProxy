# ChatGPTProxy

Hi! Thank you for looking into this code
it's not probably the best code, i mean
i don't really know if it's stable enough
to be used for production, i made it "as it is"
it's just an concept to show that you can use ChatGPT
on older hardware.

Disclaimer: reading this code may cause brain damage, so read it at own risk


How this "proxy" actually works.

Well it's actually pretty simple
1. the client sends the apikey and message(question to ChatGPT) to server
2. server parses the string sent from client and if ChatGPT returns error then server returns "ERROR" but if it will succeed
then server gonna respond with: Elapsed time of response in ms, response, Total tokens, Completion tokens and Prompt tokens
3. The client disconnects and does the own stuff like adding the message response to console/form or where do you want to store the message

i also made an basic client for this proxy here: <a href="https://github.com/Zordon1337/ChatGPT-Proxy-Client/blob/master/README.md">https://github.com/Zordon1337/ChatGPT-Proxy-Client/blob/master/README.md</a>
