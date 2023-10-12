from flask import Flask, request, jsonify
import requests

app = Flask(__name__)

API_KEY = "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"
API_URL = "https://api.openai.com/v1/engines/davinci/completions"
HEADERS = {
    "Authorization": f"Bearer {API_KEY}",
    "Content-Type": "application/json"
}

class ChatGPT:
    def ask(self, question):
        data = {
            "prompt": question,
            "max_tokens": 150
        }
        response = requests.post(API_URL, headers=HEADERS, json=data)
        return response.json()["choices"][0]["text"].strip()

chatGPT = ChatGPT()

@app.route("/chatgpt/question", methods=["POST"])
def question():
    args = request.args
    prompt = request.json
    question_text = prompt["question"]

    if args.get("debug", default=False, type=bool):
        print("ChatGPT Question Received...")
        print("ChatGPT Question is: {}".format(question_text))

    response = chatGPT.ask(question_text)

    if args.get("debug", default=False, type=bool):
        print("ChatGPT Response Received...")
        print(response)

    return jsonify(response=response)

@app.route("/chatgpt/status", methods=["GET"])
def status():
    return jsonify(status="ok")

if __name__ == "__main__":
    app.run(threaded=False)
