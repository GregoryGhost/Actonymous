#!/usr/bin/env python
# pylint: disable=unused-argument, wrong-import-position
# This program is dedicated to the public domain under the CC0 license.

"""
Simple Bot to reply to Telegram messages.

First, a few handler functions are defined. Then, those functions are passed to
the Application and registered at their respective places.
Then, the bot is started and runs until we press Ctrl-C on the command line.

Usage:
Basic Echobot example, repeats messages.
Press Ctrl-C on the command line or send a signal to the process to stop the
bot.
"""

import logging
from jira_tasks_reporter_domain import get_full_work_report_for_today, get_full_work_report_for_yesterday, get_full_work_report_for_date

from telegram import __version__ as TG_VER

try:
    from telegram import __version_info__
except ImportError:
    __version_info__ = (0, 0, 0, 0, 0)  # type: ignore[assignment]

if __version_info__ < (20, 0, 0, "alpha", 1):
    raise RuntimeError(
        f"This example is not compatible with your current PTB version {TG_VER}. To view the "
        f"{TG_VER} version of this example, "
        f"visit https://docs.python-telegram-bot.org/en/v{TG_VER}/examples.html"
    )
from telegram import ForceReply, Update
from telegram.ext import Application, CommandHandler, ContextTypes, MessageHandler, filters

# Enable logging
logging.basicConfig(
    format="%(asctime)s - %(name)s - %(levelname)s - %(message)s", level=logging.INFO
)
logger = logging.getLogger(__name__)


# Define a few command handlers. These usually take the two arguments update and
# context.
async def start(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Send a message when the command /start is issued."""
    user = update.effective_user
    await update.message.reply_html(
        rf"Hi {user.mention_html()}!",
        reply_markup=ForceReply(selective=True),
    )


async def help_command(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Send a message when the command /help is issued."""
    await update.message.reply_text("Help!")


async def echo(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Echo the user message."""
    await update.message.reply_text(update.message.text, parse_mode='markdown')


async def get_work_report_for_today(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Get a work report for the date"""
    await update.message.reply_text(get_full_work_report_for_today(), parse_mode='markdown')

async def get_work_report_for_yesterday(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Get a work report for yesterday"""
    await update.message.reply_text(get_full_work_report_for_yesterday(), parse_mode='markdown')

async def get_work_report_for_date(update: Update, context: ContextTypes.DEFAULT_TYPE) -> None:
    """Get a work report for yesterday"""
    await update.message.reply_text(get_full_work_report_for_date(), parse_mode='markdown')

def main() -> None:
    """Start the bot."""
    # Create the Application and pass it your bot's token.
    application = Application.builder().token("TOKEN_HERE").build()

    # on different commands - answer in Telegram
    application.add_handler(CommandHandler("start", start))
    application.add_handler(CommandHandler("help", help_command))
    application.add_handler(CommandHandler("get_work_report_for_today", get_work_report_for_today))
    application.add_handler(CommandHandler("get_work_report_for_yesterday", get_work_report_for_yesterday))
    application.add_handler(CommandHandler("get_work_report_for_date", get_work_report_for_date))

    # on non command i.e message - echo the message on Telegram
    application.add_handler(MessageHandler(filters.TEXT & ~filters.COMMAND, echo))

    # Run the bot until the user presses Ctrl-C
    application.run_polling()


if __name__ == "__main__":
    main()