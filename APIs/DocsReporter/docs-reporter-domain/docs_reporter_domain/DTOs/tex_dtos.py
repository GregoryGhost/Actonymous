from dataclasses import dataclass
from typing import final
import jinja2


@dataclass
class JinjaTemplate:
    template: jinja2.Template
    template_name: str = ""

JinjaTemplates = list[JinjaTemplate]

@final
@dataclass
class ActJinjaTemplate(JinjaTemplate):
    pass


@final
@dataclass
class TaskJinjaTemplate(JinjaTemplate):
    pass
