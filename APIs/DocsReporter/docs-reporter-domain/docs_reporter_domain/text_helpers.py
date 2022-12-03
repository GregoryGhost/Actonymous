from typing import final
from dataclasses import dataclass
import jinja2
from pathlib import Path
import os
from abc import ABCMeta, abstractmethod
import jinja2
from jira_helpers import MappedUserWorklog
from typing import final


def get_doc_template(filename: str) -> JinjaTemplate:
    templateLoader = jinja2.FileSystemLoader(searchpath="./templates/")
    templateEnv = jinja2.Environment(loader=templateLoader)
    template = templateEnv.get_template(filename)
    template_name = Path(filename).stem
    jinja_template = JinjaTemplate(template=template, template_name=template_name)

    return jinja_template


def get_file_path_in_cur_dir(file_name_path: Path) -> Path:
    current_dir = os.path.abspath("")
    path = Path(current_dir, file_name_path)

    return path


RenderedTexData = str


def get_tex_path(template_name: str) -> Path:
    file_name = f"{template_name}.tex"
    file_path = Path("generated_tex", file_name)

    return file_path


def write_tex_file(template_name: str, data: RenderedTexData) -> Path:
    file_name_path = get_tex_path(template_name)
    path = get_file_path_in_cur_dir(file_name_path)
    with open(path, "w", encoding="utf-8") as fp:
        fp.write(data)

    return path


def get_act_doc_template() -> JinjaTemplate:
    template = get_doc_template("act.jinja")
    act_template = ActJinjaTemplate(
        template=template.template, template_name=template.template_name
    )

    return act_template


def get_task_doc_template() -> JinjaTemplate:
    template = get_doc_template("task.jinja")
    act_template = TaskJinjaTemplate(
        template=template.template, template_name=template.template_name
    )

    return act_template


@dataclass
class ITemplateInfo(metaclass=ABCMeta):
    contract_parties_info: ContractPartiesInfo
    doc_template: JinjaTemplate
    contracts_info: ContractsInfo
    worklogs: MappedUserWorklogs

    @abstractmethod
    def get_template_data(self) -> object:
        raise NotImplementedError

    @final
    def render_template(self) -> RenderedTexData:
        template_data = self.get_template_data()
        rendered_tex_data = self.doc_template.template.render(data=template_data)

        return rendered_tex_data


def format_tex_file_by_doc_template(info: ITemplateInfo) -> Path:
    rendered_tex_data = info.render_template()
    wrotten_tex_file_path = write_tex_file(
        info.doc_template.template_name, rendered_tex_data
    )

    return wrotten_tex_file_path