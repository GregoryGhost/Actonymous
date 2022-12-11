import json
import sys
import requests

MonthName = str

InclinedMonthResult = MonthName | None


def incline_month(month_name: MonthName) -> InclinedMonthResult:
    try:
        url = "https://ws3.morpher.ru/russian/declension"
        headers = {"User-Agent": "My Python script"}
        params = dict(
            s=month_name,
            format="json",
            # token= "AUTH TOKEN HERE"
        )

        response = requests.get(url=url, params=params, headers=headers)
        is_failed_response = response.status_code != 200
        if is_failed_response:
            print(
                f"Failed response({response.status_code}) to morpher on do incline month",
                file=sys.stderr,
            )

            return None

        data = json.loads(response.text)
        inclined_month = data.get("Р")

        return inclined_month
    except Exception:
        print("Unknown error on do incline month", file=sys.stderr)

        return None